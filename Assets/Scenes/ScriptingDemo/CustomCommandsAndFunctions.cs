using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
public class CustomCommandsAndFunctions : MonoBehaviour
{
    [YarnCommand("change_scene")]
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    [YarnFunction("capitalize")]
    public static string Capitalize(string s)
    {
        if(s.Length > 0)
        {
            return char.ToUpper(s[0]) + s[1..];
        }
        return "";
    }

    [YarnFunction("title_case")]
    public static string TitleCase(string s)
    {
        var words = s.Split(" ");
        StringBuilder sb = new();
        for(int i = 0; i < words.Length; ++i)
        {
            words[i] = Capitalize(words[i]);
        }
        return sb.AppendJoin(' ', words).ToString();
    }
}
