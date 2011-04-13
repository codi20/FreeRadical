using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuantumCode.Common
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Sections : IEnumerator<Section>
    {
        private List<Section> m_Sections;

        private List<Section>.Enumerator m_Enumerator;

        public Sections()
        {
            m_Sections = new List<Section>();
        }

        [IgnoreDataMember]
        public int Count
        {
            get { return m_Sections.Count; }
        }

        [DataMember]
        public List<Section> Items
        {
            get { return m_Sections; }
            set { m_Sections = value; }
        }

        public Section New(string sectionName)
        {
            if (m_Sections.Exists(s => s.Name == sectionName))
                return null;
            else
                return new Section(sectionName);
        }

        public int Add(Section target)
        {
            if (m_Sections.Exists(s => s.Name == target.Name))
                return -1;
            else
            {
                m_Sections.Add(target);
                return m_Sections.Count - 1;
            }
        }

        public void Remove(string sectionName)
        {
            Section target = m_Sections.SingleOrDefault(s => s.Name == sectionName);
            if (null != target)
                m_Sections.Remove(target);
        }

        public Section this[string sectionName]
        {
            get
            {
                Section retValue = m_Sections.SingleOrDefault(s => s.Name == sectionName);
                return retValue;
            }
        }

        public Section this[int index]
        {
            get
            {
                Section retValue = null;
                if (0 <= index && index < m_Sections.Count)
                {
                    retValue = m_Sections[index];
                }
                
                return retValue;
            }
        }

        public bool Exists(string sectionName)
        {
            return m_Sections.Exists(s => s.Name == sectionName);
        }

        #region IEnumerator<Section> Members

        public Section Current
        {
            get
            {
                return m_Enumerator.Current;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            m_Sections.Clear();
            m_Sections = null;
            m_Enumerator.Dispose();
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return m_Enumerator.Current; }
        }

        public bool MoveNext()
        {
            return m_Enumerator.MoveNext();
        }

        public void Reset()
        {
            m_Enumerator = m_Sections.GetEnumerator();
        }

        #endregion

        public List<Section>.Enumerator GetEnumerator()
        {
            m_Enumerator = m_Sections.GetEnumerator();
            return m_Enumerator;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Section
    {
        private string m_Name;
        private List<KeyValuePair> m_KeyValues;

        public Section()
            : this("")
        {
        }

        public Section(string name)
        {
            m_Name = name;
            m_KeyValues = new List<KeyValuePair>();
        }

        public string this[string key]
        {
            get
            {
                KeyValuePair kvp = m_KeyValues.SingleOrDefault(kv => kv.Key == key);
                if (null != kvp)
                    return kvp.Value;
                return string.Empty;
            }
        }

        [DataMember]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        [DataMember]
        public List<KeyValuePair> KeyValues
        {
            get { return m_KeyValues; }
            set { m_KeyValues = value; }
        }

        public KeyValuePair New(string key, string value)
        {
            KeyValuePair retValue = null;

            if (m_KeyValues.Exists(k => k.Key == key))
            {
                return null;
            }
            else
            {
                retValue = new KeyValuePair(key, value);
                m_KeyValues.Add(retValue);
            }

            return retValue;
        }

        public bool Exists(string key)
        {
            return m_KeyValues.Exists(kv => kv.Key == key);
        }

        public string Value(string key)
        {
            KeyValuePair kv = m_KeyValues.SingleOrDefault(k => k.Key == key);

            if (null != kv)
                return kv.Value;
            else
                return string.Empty;
        }

        public int Add(string key, string value)
        {
            return Add(new KeyValuePair(key, value));
        }

        public int Add(KeyValuePair keyValuePair)
        {
            if (m_KeyValues.Exists(kv => kv.Key == keyValuePair.Key))
                return -1;
            else
            {
                m_KeyValues.Add(keyValuePair);

                return m_KeyValues.Count - 1;
            }
        }

        public void Remove(string key)
        {
            KeyValuePair target = m_KeyValues.SingleOrDefault(kv => kv.Key == key);
            if (null != target)
                m_KeyValues.Remove(target);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class KeyValuePair
    {
        private string m_Key;
        private string m_Value;

        public KeyValuePair()
        {
            m_Key = "";
            m_Value = "";
        }

        public KeyValuePair(string key, string value)
        {
            m_Key = key;
            m_Value = value;
        }

        [DataMember]
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        [DataMember]
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
    }
}
