using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditor
{
	public class PBXProject : PBXObject
	{
		protected string MAINGROUP_KEY = "mainGroup";
		protected string KNOWN_REGIONS_KEY = "knownRegions";
        protected string ATTRIBUTES = "attributes";
        protected string TARGET_ATTRIBUTES = "TargetAttributes";

		protected bool _clearedLoc = false;

		public PBXProject() : base() {
		}
		
		public PBXProject( string guid, PBXDictionary dictionary ) : base( guid, dictionary ) {
		}
		
		public string mainGroupID {
			get {
				return (string)_data[ MAINGROUP_KEY ];
			}
		}

		public PBXList knownRegions {
			get {
				return (PBXList)_data[ KNOWN_REGIONS_KEY ];
			}
		}

        public PBXDictionary attributes
        {
            get {
                return (PBXDictionary)_data[ATTRIBUTES];
            }
        }

		public void AddRegion(string region) {
			if (!_clearedLoc)
			{
				// Only include localizations we explicitly specify
				knownRegions.Clear();
				_clearedLoc = true;
			}

			knownRegions.Add(region);
		}

        public void AddAttribute(string target, Hashtable dictionary)
        {
            Debug.Log("Add attribute: " + target);
            PBXDictionary targetAttributes = (PBXDictionary)attributes[TARGET_ATTRIBUTES];
            PBXDictionary value = null;
            if (targetAttributes.ContainsKey(target)) {
                value = (PBXDictionary)targetAttributes[target];
            } else {
                targetAttributes[target] = value = new PBXDictionary();
            }
            foreach (DictionaryEntry item in dictionary) {
                value.Add((string)item.Key, item.Value);
            }
        }
	}
}
