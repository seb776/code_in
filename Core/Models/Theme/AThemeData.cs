using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;
namespace code_in.Models.Theme
{

    [Serializable]
    public abstract class AThemeData
    {
        public String Name;
        public Byte[] ForegroundColor;
        public Byte[] BackgroundColor;
        public Byte[] NodeTitleColor;
        public Byte[] NodeItemColor;
        public Byte[] IntColor;
        public Byte[] FloatColor;
        public Byte[] ShortColor;
        public Byte[] CharColor;
        public Byte[] BoolColor;
        public Byte[] EnumColor;
        public Byte[] ClassColor;
        public Byte[] FuncDeclColor;
        public Byte[] DeclColor;

        public AThemeData()
        {
            Name = "Unknown";
            ForegroundColor = new Byte[4] {0,0,0,0};
            BackgroundColor = new Byte[4] {0,0,0,0};
            NodeTitleColor = new Byte[4] {0,0,0,0};
            NodeItemColor = new Byte[4] {0,0,0,0};
            IntColor = new Byte[4] {0,0,0,0};
            FloatColor = new Byte[4] {0,0,0,0};
            ShortColor = new Byte[4] {0,0,0,0};
            CharColor = new Byte[4] {0,0,0,0};
            BoolColor = new Byte[4] {0,0,0,0};
            EnumColor = new Byte[4] {0,0,0,0};
            ClassColor = new Byte[4] {0,0,0,0};
            FuncDeclColor = new Byte[4] {0,0,0,0};
            DeclColor = new Byte[4] {0,0,0,0};

        }

        public void serializeTheme(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
           //ThemeData def = new ThemeData();
            FileStream fs = new FileStream(filename, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
           XmlSerializer formatter = new XmlSerializer(typeof(AThemeData));
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

        public void CopyFrom(AThemeData t)
        {
            Name = t.Name;
            ForegroundColor = (Byte[])t.ForegroundColor.Clone();
            BackgroundColor = (Byte[])t.BackgroundColor.Clone();
            NodeTitleColor = (Byte[])t.NodeTitleColor.Clone();
            NodeItemColor = (Byte[])t.NodeItemColor.Clone();
            IntColor = (Byte[])t.IntColor.Clone();
            FloatColor = (Byte[])t.FloatColor.Clone();
            ShortColor = (Byte[])t.ShortColor.Clone();
            CharColor = (Byte[])t.CharColor.Clone();
            BoolColor = (Byte[])t.BoolColor.Clone();
            EnumColor = (Byte[])t.EnumColor.Clone();
            ClassColor = (Byte[])t.ClassColor.Clone();
            FuncDeclColor = (Byte[])t.FuncDeclColor.Clone();
            DeclColor = (Byte[])t.DeclColor.Clone();
        }
        public void deserializeTheme(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(AThemeData));

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                this.CopyFrom((AThemeData)formatter.Deserialize(fs));
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
