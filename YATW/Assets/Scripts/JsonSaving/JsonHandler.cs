using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

namespace BladeWaltz.JsonSaving
{
	public static class JsonHandler
	{
		public static void SaveToJson<T>(List<T> _toSave, string _fileName)
		{
			Debug.Log(GetPath(_fileName));
			string content = JsonHelper.ToJson<T>(_toSave.ToArray());
			WriteFile(GetPath(_fileName), content);
		}
		public static List<T> ReadFromJson<T>(string _fileName)
		{
			string content = ReadFile(GetPath(_fileName));
			if(string.IsNullOrEmpty(content) || content == "{}")
			{
				return new List<T>();
			}
			List<T> res = JsonHelper.FromJson<T>(content).ToList();
			return res;
		}
	
		public static string GetPath(string _fileName)
		{
			return Application.persistentDataPath + "/" + _fileName;
		}
		private static void WriteFile(string _path, string _content)
		{
			FileStream fileStream = new FileStream(_path, FileMode.Create);
			using(StreamWriter writer = new StreamWriter(fileStream))
			{
				writer.Write(_content);
			}
		}
		private static string ReadFile(string _path)
		{
			if(File.Exists(_path))
			{
				using(StreamReader reader = new StreamReader(_path))
				{
					string content = reader.ReadToEnd();
					return content;
				}
			}
			return "";
		}
	}
}
public static class JsonHelper
{
	public static T[] FromJson<T>(string _json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(_json);
		return wrapper.m_items;
	}
	public static string ToJson<T>(T[] _array)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.m_items = _array;
		return JsonUtility.ToJson(wrapper);
	}
	public static string ToJson<T>(T[] _array, bool _prettyPrint)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.m_items = _array;
		return JsonUtility.ToJson(wrapper, _prettyPrint);
	}
	[Serializable]
	private class Wrapper<T>
	{
		public T[] m_items;
	}
}
