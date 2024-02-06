using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class CharacterSpriteView : DialogueViewBase
{
    #region member variables
    [System.Serializable]
    class CharacterExpression
    {
        public string Expression;
        public Sprite Sprite;
    }

    [System.Serializable]
    class CharacterSprite
    {
        public string Name;
        public Sprite Sprite;
        public List<CharacterExpression> Expressions = new();
    }

    [SerializeField] List<CharacterSprite> sprites = new();
    [SerializeField] Sprite unknownSprite;

    [SerializeField] GameObject imageContainer;
    [SerializeField] Image image;

    Dictionary<string, Sprite> nameDefaultSpriteDict = new();
    Dictionary<string, Dictionary<string, Sprite>> nameExpressionSpriteDict = new();
    #endregion

    void Awake()
    {
        foreach (var sprite in sprites)
        {
            nameDefaultSpriteDict[sprite.Name] = sprite.Sprite;

            foreach(var expression in sprite.Expressions)
            {
                if(!nameExpressionSpriteDict.TryGetValue(sprite.Name, out var expressionMap)) {
                    expressionMap = new();
                    nameExpressionSpriteDict[sprite.Name] = expressionMap;
                }
                expressionMap[expression.Expression] = expression.Sprite;
            }
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        var characterName = dialogueLine.CharacterName;
        image.sprite = null;
        
        // if character name is registered
        if (characterName != null && nameDefaultSpriteDict.TryGetValue(characterName, out Sprite defaultSprite))
        {
            // check if there is metadata for expression
            // it will be in the form expression:name
            string expressionName = null;
            if (dialogueLine.Metadata != null)
            {
                foreach (var metadata in dialogueLine.Metadata)
                {
                    // if the metadata starts with "expression:"
                    // set the expression to whats after "expression:" and break
                    const string expressionPrefix = "expression:";
                    if (metadata.StartsWith(expressionPrefix))
                    {
                        expressionName = metadata[expressionPrefix.Length..];
                        break;
                    }
                }
            }
            if(expressionName != null)
            {
                // get the sprite from the expression dictionary
                if (nameExpressionSpriteDict.TryGetValue(characterName, out var expressionDictionary)
                    && expressionDictionary.TryGetValue(expressionName, out var expressionSprite))
                {
                    image.sprite = expressionSprite;
                }
                else
                {
                    Debug.LogWarning($"Could not get expression \"{expressionName}\" for character \"{characterName}\"");
                    image.sprite = defaultSprite;
                }
            }
            else
            {
                image.sprite = defaultSprite;
            }
        }
        // unknown name
        else
        {
            image.sprite = unknownSprite;
        }

        imageContainer.SetActive(image.sprite != null);

        onDialogueLineFinished();
    }
}