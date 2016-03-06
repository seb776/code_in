using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace code_in.Managers
{

    [Serializable]
    class Configuration
    {
        enum EIndentType { };
        String _selectedTheme;
        String _languagePath;
        EIndentType _indentType;
        Dictionary<int, Dictionary<int, String>> _shortcuts;

        public Configuration()
        {
            _selectedTheme = "";
            _languagePath = "";
            _shortcuts = null;
        }

        public void serializeConfiguration(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
            //ThemeData def = new ThemeData();
            FileStream fs = new FileStream(filename, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            XmlSerializer formatter = new XmlSerializer(typeof(Configuration));
            try
            {
                formatter.Serialize(fs, this);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public void deserializeConfiguration(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Configuration));

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
