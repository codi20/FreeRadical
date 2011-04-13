using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QuantumCode.Common
{
    /// <summary>
    /// 用于存储INI文件信息的类
    /// </summary>
    public class INIFile
    {
        private Sections m_IniContents;

        /// <summary>
        /// 构造函数
        /// </summary>
        public INIFile()
        {
            m_IniContents = new Sections();
        }

        /// <summary>
        /// 获取指定Section下的指定Key的值。如果没有对应的Section或Key，那么返回null
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Value(string sectionName, string key)
        {
            if (m_IniContents.Exists(sectionName))
            {
                if (m_IniContents[sectionName].Exists(key))
                    return m_IniContents[sectionName][key];
            }

            return null;
        }

        /// <summary>
        /// 添加一个Section
        /// </summary>
        /// <param name="sectionName"></param>
        public void AddSection(string sectionName)
        {
            if (!m_IniContents.Exists(sectionName))
                m_IniContents.Add(new Section(sectionName));
        }

        /// <summary>
        /// 添加一个已有的Section
        /// </summary>
        /// <param name="newSection"></param>
        public void AddSection(Section newSection)
        {
            if (!m_IniContents.Exists(newSection.Name))
                m_IniContents.Add(newSection);
        }

        /// <summary>
        /// 添加一个Key，Value对
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddValue(string section, string key, string value)
        {
            AddSection(section);

            if (!m_IniContents[section].Exists(key))
                m_IniContents[section].Add(key, value);
        }

        /// <summary>
        /// 移除一个Key，Value对
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        public void RemoveValue(string section, string key)
        {
            if (m_IniContents.Exists(section))
            {
                if (m_IniContents[section].Exists(key))
                {
                    m_IniContents[section].Remove(key);
                }
            }
        }

        /// <summary>
        /// 移除一个Section
        /// </summary>
        /// <param name="section"></param>
        public void RemoveSection(string section)
        {
            if (m_IniContents.Exists(section))
            {
                m_IniContents.Remove(section);
            }
        }

        /// <summary>
        /// 获取Section列表
        /// </summary>
        public List<string> Sections
        {
            get
            {
                if (0 == m_IniContents.Count)
                    return new List<string>();
                else
                    return m_IniContents.Items.Select(s => s.Name).ToList();
            }
        }

        public Section this[string sectionName]
        {
            get
            {
                if (m_IniContents.Exists(sectionName))
                    return m_IniContents[sectionName];
                else
                    return null;
            }
        }

        public Section this[int index]
        {
            get
            {
                Section retValue = null;
                if (0 <= index && index < m_IniContents.Count)
                {
                    retValue = m_IniContents[index];
                }
                return retValue;
            }
        }

        /// <summary>
        /// 获取Section下的Key列表
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public List<string> GetKeys(string sectionName)
        {
            if (m_IniContents.Exists(sectionName))
            {
                return m_IniContents[sectionName].KeyValues.Select(kv => kv.Key).ToList();
            }
            else
                return new List<string>();
        }

        public static INIFile ReadINI(string fullFileName)
        {
            if (File.Exists(fullFileName))
            {
                FileStream fs = new FileStream(fullFileName, FileMode.Open);
                StreamReader sReader = new StreamReader(fs);

                try
                {
                    INIFile retValue = new INIFile();

                    string section = ReadNextSection(sReader, null);

                    while (null != section)
                    {
                        Section newSection = new Section(section);
                        section = ReadNextSection(sReader, newSection);
                        retValue.AddSection(newSection);
                    }

                    return retValue;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if(null != sReader)
                        sReader.Close();

                    if(null != fs)
                        fs.Close();
                }
            }
            else
            {
                throw new FileNotFoundException("找不到指定的文件:" + fullFileName);
            }
        }

        protected static string ReadNextSection(StreamReader sReader, Section currentSection)
        {
            string line = string.Empty;

            while (!sReader.EndOfStream)
            {
                line = sReader.ReadLine();

                if (!string.IsNullOrEmpty(line.Trim()))
                {
                    if (line.StartsWith("["))
                        return line.Substring(1, line.Length - 2);
                    else
                    {
                        if (!line.StartsWith(";") && line.Contains("=") && null != currentSection)
                        {
                            string[] kp = line.Split(new char[] { '=' });

                            currentSection.Add(kp[0], kp[1]);
                        }

                    }
                }
            }

            return null;
        }
    }
}
