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

        //BaseNode
        public Byte[] BNTypeForeGroundColor;
        public Byte[] BNSeparatorForeGroundColor;
        public Byte[] BNNameForeGroundColor;
        public Byte[] BNMainColor;
        public Byte[] BNSecondaryColor;

        //EnumNode
        public Byte[] ENTypeForeGroundColor;
        public Byte[] ENSeparatorForeGroundColor;
        public Byte[] ENNameForeGroundColor;
        public Byte[] ENMainColor;
        public Byte[] ENSecondaryColor;

        //ClassDeclNode
        public Byte[] CDNTypeForeGroundColor;
        public Byte[] CDNSeparatorForeGroundColor;
        public Byte[] CDNNameForeGroundColor;
        public Byte[] CDNMainColor;
        public Byte[] CDNSecondaryColor;

        //InterfaceDeclNode
        public Byte[] IDNTypeForeGroundColor;
        public Byte[] IDNSeparatorForeGroundColor;
        public Byte[] IDNNameForeGroundColor;
        public Byte[] IDNMainColor;
        public Byte[] IDNSecondaryColor;

        //NamespaceNode
        public Byte[] NNTypeForeGroundColor;
        public Byte[] NNSeparatorForeGroundColor;
        public Byte[] NNNameForeGroundColor;
        public Byte[] NNMainColor;
        public Byte[] NNSecondaryColor;

        //FuncEntryNode
        public Byte[] FENTypeForeGroundColor;
        public Byte[] FENSeparatorForeGroundColor;
        public Byte[] FENNameForeGroundColor;
        public Byte[] FENMainColor;
        public Byte[] FENSecondaryColor;

        //FuncDeclNode
        public Byte[] FDNTypeForeGroundColor;
        public Byte[] FDNSeparatorForeGroundColor;
        public Byte[] FDNNameForeGroundColor;
        public Byte[] FDNMainColor;
        public Byte[] FDNSecondaryColor;

        //ReturnStmtNode
        public Byte[] RSNTypeForeGroundColor;
        public Byte[] RSNSeparatorForeGroundColor;
        public Byte[] RSNNameForeGroundColor;
        public Byte[] RSNMainColor;
        public Byte[] RSNSecondaryColor;

        //VarDeclStmtNode
        public Byte[] VDSNTypeForeGroundColor;
        public Byte[] VDSNSeparatorForeGroundColor;
        public Byte[] VDSNNameForeGroundColor;
        public Byte[] VDSNMainColor;
        public Byte[] VDSNSecondaryColor;

        //DefaultStmtNode
        public Byte[] DSNTypeForeGroundColor;
        public Byte[] DSNSeparatorForeGroundColor;
        public Byte[] DSNNameForeGroundColor;
        public Byte[] DSNMainColor;
        public Byte[] DSNSecondaryColor;

        //IfStmtNode
        public Byte[] ISNTypeForeGroundColor;
        public Byte[] ISNSeparatorForeGroundColor;
        public Byte[] ISNNameForeGroundColor;
        public Byte[] ISNMainColor;
        public Byte[] ISNSecondaryColor;

        //WhileStmtNode
        public Byte[] WSNTypeForeGroundColor;
        public Byte[] WSNSeparatorForeGroundColor;
        public Byte[] WSNNameForeGroundColor;
        public Byte[] WSNMainColor;
        public Byte[] WSNSecondaryColor;

        //ForStmtNode
        public Byte[] FSNTypeForeGroundColor;
        public Byte[] FSNSeparatorForeGroundColor;
        public Byte[] FSNNameForeGroundColor;
        public Byte[] FSNMainColor;
        public Byte[] FSNSecondaryColor;

        //DoWhileStmtNode
        public Byte[] DWSNTypeForeGroundColor;
        public Byte[] DWSNSeparatorForeGroundColor;
        public Byte[] DWSNNameForeGroundColor;
        public Byte[] DWSNMainColor;
        public Byte[] DWSNSecondaryColor;

        //ForEachStmtNode
        public Byte[] FESNTypeForeGroundColor;
        public Byte[] FESNSeparatorForeGroundColor;
        public Byte[] FESNNameForeGroundColor;
        public Byte[] FESNMainColor;
        public Byte[] FESNSecondaryColor;

        //UnaryExprNode
        public Byte[] UENTypeForeGroundColor;
        public Byte[] UENSeparatorForeGroundColor;
        public Byte[] UENNameForeGroundColor;
        public Byte[] UENMainColor;
        public Byte[] UENSecondaryColor;

        //BinaryExprNode
        public Byte[] BENTypeForeGroundColor;
        public Byte[] BENSeparatorForeGroundColor;
        public Byte[] BENNameForeGroundColor;
        public Byte[] BENMainColor;
        public Byte[] BENSecondaryColor;

        //TernaryExprNode
        public Byte[] TENTypeForeGroundColor;
        public Byte[] TENSeparatorForeGroundColor;
        public Byte[] TENNameForeGroundColor;
        public Byte[] TENMainColor;
        public Byte[] TENSecondaryColor;

        //FuncExprNode
        public Byte[] FExNTypeForeGroundColor;
        public Byte[] FExNSeparatorForeGroundColor;
        public Byte[] FExNNameForeGroundColor;
        public Byte[] FExNMainColor;
        public Byte[] FExNSecondaryColor;

        /*
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
        */
        public AThemeData()
        {
            Name = "Unknown";

            //BaseNode
            BNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            BNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            BNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            BNMainColor = new Byte[4] { 0, 0, 0, 0 };
            BNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //EnumNode
            ENTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            ENSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            ENNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            ENMainColor = new Byte[4] { 0, 0, 0, 0 };
            ENSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //ClassDeclNode
            CDNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            CDNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            CDNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            CDNMainColor = new Byte[4] { 0, 0, 0, 0 };
            CDNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //InterfaceDeclNode
            IDNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            IDNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            IDNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            IDNMainColor = new Byte[4] { 0, 0, 0, 0 };
            IDNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //NamespaceNode
            NNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            NNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            NNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            NNMainColor = new Byte[4] { 0, 0, 0, 0 };
            NNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //FuncEntryNode
            FENTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FENSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FENNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FENMainColor = new Byte[4] { 0, 0, 0, 0 };
            FENSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //FuncDeclNode
            FDNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FDNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FDNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FDNMainColor = new Byte[4] { 0, 0, 0, 0 };
            FDNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //ReturnStmtNode
            RSNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            RSNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            RSNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            RSNMainColor = new Byte[4] { 0, 0, 0, 0 };
            RSNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //VarDeclStmtNode
            VDSNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            VDSNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            VDSNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            VDSNMainColor = new Byte[4] { 0, 0, 0, 0 };
            VDSNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //DefaultStmtNode
            DSNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            DSNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            DSNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            DSNMainColor = new Byte[4] { 0, 0, 0, 0 };
            DSNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //IfStmtNode
            ISNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            ISNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            ISNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            ISNMainColor = new Byte[4] { 0, 0, 0, 0 };
            ISNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //WhileStmtNode
            WSNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            WSNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            WSNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            WSNMainColor = new Byte[4] { 0, 0, 0, 0 };
            WSNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //ForStmtNode
            FSNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FSNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FSNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FSNMainColor = new Byte[4] { 0, 0, 0, 0 };
            FSNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //DoWhileStmtNode
            DWSNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            DWSNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            DWSNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            DWSNMainColor = new Byte[4] { 0, 0, 0, 0 };
            DWSNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //ForEachStmtNode
            FESNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FESNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FESNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FESNMainColor = new Byte[4] { 0, 0, 0, 0 };
            FESNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //UnaryExprNode
            UENTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            UENSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            UENNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            UENMainColor = new Byte[4] { 0, 0, 0, 0 };
            UENSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //BinaryExprNode
            BENTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            BENSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            BENNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            BENMainColor = new Byte[4] { 0, 0, 0, 0 };
            BENSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //TernaryExprNode
            TENTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            TENSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            TENNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            TENMainColor = new Byte[4] { 0, 0, 0, 0 };
            TENSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            //FuncExprNode
            FExNTypeForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FExNSeparatorForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FExNNameForeGroundColor = new Byte[4] { 0, 0, 0, 0 };
            FExNMainColor = new Byte[4] { 0, 0, 0, 0 };
            FExNSecondaryColor = new Byte[4] { 0, 0, 0, 0 };

            /*
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
            DeclColor = new Byte[4] {0,0,0,0}; */

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
            /*
            BNTypeForegroundColor = (Byte[])t.BNTypeForegroundColor.Clone();
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
              */
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

        //BaseNode setters
        public void setBNTypeForegroundColor()
        {

        }
        public void setBNSeparatorForegroundColor()
        {

        }
        public void setBNNameForegroundColor()
        {

        }
        public void setBNMainColor()
        {

        }
        public void setBNSecondaryColor()
        {

        }

        //EnumNode setters
        public void setENTypeForegroundColor()
        {

        }
        public void setENSeparatorForegroundColor()
        {

        }
        public void setENNameForegroundColor()
        {

        }
        public void setENMainColor()
        {

        }
        public void setENSecondaryColor()
        {

        }

        //ClassDecl setters
        public void setCDNTypeForegroundColor()
        {

        }
        public void setCDNSeparatorForegroundColor()
        {

        }

        public void setCDNNameForegroundColor()
        {

        }
        public void setCDNTMainColor()
        {

        }
        public void setCDNSecondaryColor()
        {

        }

        //InterfaceDecl setters
        public void setIDNTypeForegroundColor()
        {

        }

        public void setIDNSeparatorForegroundColor()
        {

        }
        public void setIDNNameForegroundColor()
        {

        }
        public void setIDNMainColor()
        {

        }
        public void setIDNSecondaryColor()
        {

        }

        //NamespaceNode setters
        public void setNNTypeForegroundColor()
        {

        }

        public void setNNSeparatorForegroundColor()
        {

        }
        public void setNNNameForegroundColor()
        {

        }
        public void setNNMainColor()
        {

        }
        public void setNNSecondaryColor()
        {

        }

        //FuncEntryNode setters
        public void setFENTypeForegroundColor()
        {

        }

        public void setFENSeparatorForegroundColor()
        {

        }
        public void setFENNameForegroundColor()
        {

        }
        public void setFENMainColor()
        {

        }
        public void setFENSecondaryColor()
        {

        }

        //FuncDeclNode setters
        public void setFDNTypeForegroundColor()
        {

        }

        public void setFDNSeparatorForegroundColor()
        {

        }
        public void setFDNNameForegroundColor()
        {

        }
        public void setFDNMainColor()
        {

        }
        public void setFDNSeparatorColor()
        {

        }

        //ReturnStmtNode setters
        public void setRSNTypeForegroundColor()
        {

        }

        public void setRSNSeparatorForegroundColor()
        {

        }
        public void setRSNNameForegroundColor()
        {

        }
        public void setRSNMainColor()
        {

        }
        public void setRSNSecondaryColor()
        {

        }

        //VarDeclStmtNode setters
        public void setVDSNTypeForegroundColor()
        {

        }

        public void setVDSNSeparatorForegroundColor()
        {

        }
        public void setVDSNNameForegroundColor()
        {

        }
        public void setVDSNMainColor()
        {

        }
        public void setVDSNSecondaryColor()
        {

        }

        //DefaultStmtNode setters
        public void setDSNTypeForegroundColor()
        {

        }

        public void setDSNSeparatorForegroundColor()
        {

        }
        public void setDSNNameForegroundColor()
        {

        }
        public void setDSNMainColor()
        {

        }
        public void setDSNSecondaryColor()
        {

        }

        //IfStmtNode setters
        public void setISNTypeForegroundColor()
        {

        }

        public void setISNSeparatorForegroundColor()
        {

        }
        public void setISNNameForegroundColor()
        {

        }
        public void setISNMainColor()
        {

        }
        public void setISNTypeSecondaryColor()
        {

        }

        //WhileStmtNode setters
        public void setWSNTypeForegroundColor()
        {

        }

        public void setWSNSeparatorForegroundColor()
        {

        }
        public void setWSNNameForegroundColor()
        {

        }
        public void setWSNMainColor()
        {

        }
        public void setWSNSecondaryColor()
        {

        }

        //ForStmtNode setters
        public void setFSNTypeForegroundColor()
        {

        }

        public void setFSNSeparatorForegroundColor()
        {

        }
        public void setFSNNameForegroundColor()
        {

        }
        public void setFSNMainColor()
        {

        }
        public void setFSNSecondaryColor()
        {

        }

        //DoWhileStmtNode setters
        public void setDWSNTypeForegroundColor()
        {

        }

        public void setDWSNSeparatorForegroundColor()
        {

        }
        public void setDWSNNameForegroundColor()
        {

        }
        public void setDWSNMainColor()
        {

        }
        public void setDWSNSecondaryColor()
        {

        }

        //ForeachStmtNode setters
        public void setFESNTypeForegroundColor()
        {

        }

        public void setFESNSeparatorForegroundColor()
        {

        }
        public void setFESNNameForegroundColor()
        {

        }
        public void setFESNMainColor()
        {

        }
        public void setFESNSecondaryColor()
        {

        }

        //UnaryExprNode setters
        public void setUENTypeForegroundColor()
        {

        }

        public void setUENSeparatorForegroundColor()
        {

        }
        public void setUENNameForegroundColor()
        {

        }
        public void setUENMainColor()
        {

        }
        public void setUENSecondaryColor()
        {

        }

        //BinaryExprNode setters
        public void setBENTypeForegroundColor()
        {

        }

        public void setBENSeparatorForegroundColor()
        {

        }
        public void setBENNameForegroundColor()
        {

        }
        public void setBENMainColor()
        {

        }
        public void setBENSecondaryColor()
        {

        }

        //TernaryExprNode setters
        public void setTENTypeForegroundColor()
        {

        }

        public void setTENSeparatorForegroundColor()
        {

        }
        public void setTENNameForegroundColor()
        {

        }
        public void setTENMainColor()
        {

        }
        public void setTENSecondaryColor()
        {

        }

        //FuncExprNode setters
        public void setFExNTypeForegroundColor()
        {

        }

        public void setFExNSeparatorForegroundColor()
        {

        }
        public void setFExNNameForegroundColor()
        {

        }
        public void setFExNMainColor()
        {

        }
        public void setFExNSecondaryColor()
        {

        }


    }
}
