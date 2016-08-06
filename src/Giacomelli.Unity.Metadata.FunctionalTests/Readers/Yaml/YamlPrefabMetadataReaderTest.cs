using System;
using NUnit.Framework;
using System.IO;

namespace Giacomelli.Unity.Metadata.FunctionalTests
{
	[TestFixture]
	public class YamlPrefabMetadataReaderTest
	{
		[Test]
		public void Read_ValidFile_Metadata ()
		{
			var target = new YamlPrefabMetadataReader ();
			var yaml = File.OpenText ("Readers/Yaml/PrefabSample1.prefab");
			var actual = target.Read (yaml);

			Assert.IsNotNull (actual);
			Assert.AreEqual (2, actual.MonoBehaviours.Count);

			// First MonoBehaviour.
			var actualMonoBehaviour = actual.MonoBehaviours [0];
			Assert.IsNotNull (actualMonoBehaviour);

			var actualScript = actualMonoBehaviour.Script;
			Assert.IsNotNull (actualScript);
			Assert.AreEqual ("1363015578", actualScript.FileId);
	
			// Second MonoBehaviour.
			actualMonoBehaviour = actual.MonoBehaviours [1];
			Assert.IsNotNull (actualMonoBehaviour);

			actualScript = actualMonoBehaviour.Script;
			Assert.IsNotNull (actualScript);
			Assert.AreEqual ("-1557964980", actualScript.FileId);
		}
	}
}

