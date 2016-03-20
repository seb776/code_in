using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace code_in.ViewModels
{
    public class ThemeMgr
    {
        public ThemeMgr() {
        }

        private void checkResourceTheme()
        {
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["BaseNodeColor"] != null, "No BaseNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["BaseNodeColorBack"] != null, "No BaseNodeColorBack in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["NamespaceNodeColor"] != null, "No NamespaceNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["FuncDeclNodeColor"] != null, "No FuncDeclNodeColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["PreprocessColor"] != null, "No PreprocessColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["IntColor"] != null, "No IntColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["CharColor"] != null, "No CharColor in dictionnary");
            System.Diagnostics.Debug.Assert(Resources.SharedDictionaryManager.MainResourceDictionary["FloatingColor"] != null, "No FloatingColor in dictionnary");
        }


        public void setTheme(System.Windows.ResourceDictionary resDict, Models.Theme.AThemeData data) {
            checkResourceTheme();

            // Put random color to each resources of the dictionary
            // Have to take by the next colors in the given class code_inMgr

            /*
            resDict["BaseNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BNTypeForeGroundColor));
            resDict["BaseNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BNSeparatorForegroungColor));
            resDict["NamespaceNodeColor"] = new SolidColorBrush(Colors.Indigo);
            resDict["FuncDeclNodeColor"] = new SolidColorBrush(Colors.Yellow);
            resDict["PreprocessColor"] = new SolidColorBrush(Colors.Tomato);
            resDict["IntColor"] = new SolidColorBrush(Colors.OliveDrab);
            resDict["CharColor"] = new SolidColorBrush(Colors.Bisque);
            resDict["FloatingColor"] = new SolidColorBrush(Colors.HotPink); */

            resDict["BaseNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BNTypeForeGroundColor));
            resDict["BaseNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BNSeparatorForeGroundColor));
            resDict["BaseNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BNNameForeGroundColor));
            resDict["BaseNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.BNMainColor));
            resDict["BaseNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.BNSecondaryColor));

            resDict["EnumNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.ENTypeForeGroundColor));
            resDict["EnumNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.ENSeparatorForeGroundColor));
            resDict["EnumNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.ENNameForeGroundColor));
            resDict["EnumNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.ENMainColor));
            resDict["EnumNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.ENSecondaryColor));

            resDict["ClassDeclNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.CDNTypeForeGroundColor));
            resDict["ClassDeclNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.CDNSeparatorForeGroundColor));
            resDict["ClassDeclNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.CDNNameForeGroundColor));
            resDict["ClassDeclNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.CDNMainColor));
            resDict["ClassDeclNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.CDNSecondaryColor));

            resDict["InterfaceDeclNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.IDNTypeForeGroundColor));
            resDict["InterfaceDeclNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.IDNSeparatorForeGroundColor));
            resDict["InterfaceDeclNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.IDNNameForeGroundColor));
            resDict["InterfaceDeclNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.IDNMainColor));
            resDict["InterfaceDeclNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.IDNSecondaryColor));

            resDict["NamespaceNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.NNTypeForeGroundColor));
            resDict["NamespaceNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.NNSeparatorForeGroundColor));
            resDict["NamespaceNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.NNNameForeGroundColor));
            resDict["NamespaceNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.NNMainColor));
            resDict["NamespaceNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.NNSecondaryColor));

            resDict["FuncEntryNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FENTypeForeGroundColor));
            resDict["FuncEntryNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FENSeparatorForeGroundColor));
            resDict["FuncEntryNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FENNameForeGroundColor));
            resDict["FuncEntryNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.FENMainColor));
            resDict["FuncEntryNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.FENSecondaryColor));

            resDict["FuncDeclNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FDNTypeForeGroundColor));
            resDict["FuncDeclNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FDNSeparatorForeGroundColor));
            resDict["FuncDeclNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FDNNameForeGroundColor));
            resDict["FuncDeclNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.FDNMainColor));
            resDict["FuncDeclNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.FDNSecondaryColor));

            resDict["ReturnStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.RSNTypeForeGroundColor));
            resDict["ReturnStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.RSNSeparatorForeGroundColor));
            resDict["ReturnStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.RSNNameForeGroundColor));
            resDict["ReturnStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.RSNMainColor));
            resDict["ReturnStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.RSNSecondaryColor));

            resDict["VarDeclStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.VDSNTypeForeGroundColor));
            resDict["VarDeclStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.VDSNSeparatorForeGroundColor));
            resDict["VarDeclStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.VDSNNameForeGroundColor));
            resDict["VarDeclStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.VDSNMainColor));
            resDict["VarDeclStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.VDSNSecondaryColor));

            resDict["DefaultStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.DSNTypeForeGroundColor));
            resDict["DefaultStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.DSNSeparatorForeGroundColor));
            resDict["DefaultStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.DSNNameForeGroundColor));
            resDict["DefaultStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.DSNMainColor));
            resDict["DefaultStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.DSNSecondaryColor));

            resDict["IfStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.ISNTypeForeGroundColor));
            resDict["IfStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.ISNSeparatorForeGroundColor));
            resDict["IfStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.ISNNameForeGroundColor));
            resDict["IfStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.ISNMainColor));
            resDict["IfStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.ISNSecondaryColor));

            resDict["WhileStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.WSNTypeForeGroundColor));
            resDict["WhileStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.WSNSeparatorForeGroundColor));
            resDict["WhileStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.WSNNameForeGroundColor));
            resDict["WhileStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.WSNMainColor));
            resDict["WhileStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.WSNSecondaryColor));

            resDict["ForStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FSNTypeForeGroundColor));
            resDict["ForStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FSNSeparatorForeGroundColor));
            resDict["ForStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FSNNameForeGroundColor));
            resDict["ForStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.FSNMainColor));
            resDict["ForStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.FSNSecondaryColor));

            resDict["DoWhileStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.DWSNTypeForeGroundColor));
            resDict["DoWhileStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.DWSNSeparatorForeGroundColor));
            resDict["DoWhileStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.DWSNNameForeGroundColor));
            resDict["DoWhileStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.DWSNMainColor));
            resDict["DoWhileStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.DWSNSecondaryColor));

            resDict["ForeachStmtNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FESNTypeForeGroundColor));
            resDict["ForeachStmtNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FESNSeparatorForeGroundColor));
            resDict["ForeachStmtNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FESNNameForeGroundColor));
            resDict["ForeachStmtNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.FESNMainColor));
            resDict["ForeachStmtNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.FESNSecondaryColor));

            resDict["UnaryExprNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.UENTypeForeGroundColor));
            resDict["UnaryExprNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.UENSeparatorForeGroundColor));
            resDict["UnaryExprNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.UENNameForeGroundColor));
            resDict["UnaryExprNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.UENMainColor));
            resDict["UnaryExprNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.UENSecondaryColor));

            resDict["BinaryExprNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BENTypeForeGroundColor));
            resDict["BinaryExprNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BENSeparatorForeGroundColor));
            resDict["BinaryExprNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.BENNameForeGroundColor));
            resDict["BinaryExprNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.BENMainColor));
            resDict["BinaryExprNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.BENSecondaryColor));

            resDict["TernaryExprNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.TENTypeForeGroundColor));
            resDict["TernaryExprNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.TENSeparatorForeGroundColor));
            resDict["TernaryExprNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.TENNameForeGroundColor));
            resDict["TernaryExprNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.TENMainColor));
            resDict["TernaryExprNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.TENSecondaryColor));

            resDict["FuncExprNodeTypeForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FExNTypeForeGroundColor));
            resDict["FuncExprNodeSeparatorForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FExNSeparatorForeGroundColor));
            resDict["FuncExprNodeNameForeGroundColor"] = new SolidColorBrush(setColorFromByte4(data.FExNNameForeGroundColor));
            resDict["FuncExprNodeMainColor"] = new SolidColorBrush(setColorFromByte4(data.FExNMainColor));
            resDict["FuncExprNodeSecondaryColor"] = new SolidColorBrush(setColorFromByte4(data.FExNSecondaryColor));
 
        }

        public Color setColorFromByte4(Byte[] color)
        {
            Color res = new Color();

            System.Diagnostics.Debug.Assert(color.Count() > 3);
            res.A = color[0];
            res.R = color[1];
            res.G = color[2];
            res.B = color[3];
            return res;
        }

        public void setMainTheme(Models.Theme.AThemeData data)
        {
            setTheme(Resources.SharedDictionaryManager.MainResourceDictionary, data);
        }
        public void setPreviewTheme(Models.Theme.AThemeData data)
        {
            setTheme(Resources.SharedDictionaryManager.ThemePreviewResourceDictionary, data);
        }

    }
}
