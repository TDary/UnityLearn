using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using System.IO;
using System;

namespace ProfileMemory
{
    /// <summary>
    /// Unity版本：2020.3
    /// 获取Profile的内存数据
    /// </summary>
    public class ExtractMemoryInfo
    {
        [MenuItem("Tool/ExtractMemoryDetailed")]
        public static void ExtractMemoryDetailed()
        {
            Type memoryProfilerModule = typeof(EditorWindow).Assembly.GetType("UnityEditorInternal.Profiling.MemoryProfilerModule");
            WeakReference instance = memoryProfilerModule.GetField("instance", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as WeakReference;
            object m_MemoryListView = memoryProfilerModule.GetField("m_MemoryListView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance.Target);
            Type MemoryTreeList = typeof(EditorWindow).Assembly.GetType("UnityEditor.MemoryTreeList");
            object m_Root = MemoryTreeList.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(m_MemoryListView);

            MemoryElement data = MemoryElement.Create(m_Root, -1);
            string dirName = "MemoryDetailed";
            string fileName = string.Format("MemoryDetailed{0:yyyy_MM_dd_HH_mm_ss}.txt", DateTime.Now);
            string outputPath = string.Format("{0}/{1}/{2}", System.Environment.CurrentDirectory, dirName, fileName);
            string dir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            StreamWriter writer = new StreamWriter(outputPath);
            WriteMemoryDetail(writer, data);
            writer.Flush();
            writer.Close();
            Debug.Log(string.Format("提取Profile内存数据完成（{0}）", outputPath));
        }

        public static void WriteMemoryDetail(StreamWriter writer, MemoryElement root)
        {
            if (root == null) return;
            writer.WriteLine(root.ToString());
            foreach (var memoryElement in root.children)
            {
                if (memoryElement != null)
                {
                    WriteMemoryDetail(writer, memoryElement);
                }
            }
        }
    }
    public class MemoryElement
    {
        private int depth;
        public string name;
        public int totalChildCount;
        public long totalMemory;
        public List<MemoryElement> children = new List<MemoryElement>();
        public static MemoryElement Create(object root, int depth)
        {
            if (root == null) return null;
            MemoryElement memoryElement = new MemoryElement { depth = depth };
            memoryElement.name = (string)root.GetType().GetField("name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField).GetValue(root);
            memoryElement.totalMemory = (long)root.GetType().GetField("totalMemory", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField).GetValue(root);
            memoryElement.totalChildCount = (int)root.GetType().GetField("totalChildCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField).GetValue(root);
            var children = root.GetType().GetField("children", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField).GetValue(root) as IList;
            if (children != null)
            {
                foreach (var child in children)
                {
                    if (child == null) continue;
                    //递归遍历
                    MemoryElement t_memoryElement = Create(child, depth + 1);
                    memoryElement.children.Add(t_memoryElement);
                }
            }
            return memoryElement;
        }
        public override string ToString()
        {
            if (depth < 0)
            {
                return string.Format("totalMemory:{0}B", totalMemory);
            }
            if (children.Count > 0)
            {
                string resultString = string.Format(new string('\t', depth) + "{0},({1}),{2}B", name, totalChildCount, totalMemory);
                return resultString;
            }
            else
            {
                string resultString = string.Format(new string('\t', depth) + "{0},{1}B", name, totalMemory);
                return resultString;
            }
        }
    }
}

