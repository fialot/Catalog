﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Katalog.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\database.mdf;" +
            "Integrated Security=True;Connect Timeout=30")]
        public string localDBSC {
            get {
                return ((string)(this["localDBSC"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int DefaultBorrInterval {
            get {
                return ((int)(this["DefaultBorrInterval"]));
            }
            set {
                this["DefaultBorrInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100000000000")]
        public long ContactStart {
            get {
                return ((long)(this["ContactStart"]));
            }
            set {
                this["ContactStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ContactPrefix {
            get {
                return ((string)(this["ContactPrefix"]));
            }
            set {
                this["ContactPrefix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ContactSuffix {
            get {
                return ((string)(this["ContactSuffix"]));
            }
            set {
                this["ContactSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200000000000")]
        public long ItemStart {
            get {
                return ((long)(this["ItemStart"]));
            }
            set {
                this["ItemStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("I")]
        public string ItemPrefix {
            get {
                return ((string)(this["ItemPrefix"]));
            }
            set {
                this["ItemPrefix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ItemSuffix {
            get {
                return ((string)(this["ItemSuffix"]));
            }
            set {
                this["ItemSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public int ContactMinCharLen {
            get {
                return ((int)(this["ContactMinCharLen"]));
            }
            set {
                this["ContactMinCharLen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public int ItemMinCharLen {
            get {
                return ((int)(this["ItemMinCharLen"]));
            }
            set {
                this["ItemMinCharLen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IncSpecimenInv {
            get {
                return ((bool)(this["IncSpecimenInv"]));
            }
            set {
                this["IncSpecimenInv"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300000000000")]
        public long BookStart {
            get {
                return ((long)(this["BookStart"]));
            }
            set {
                this["BookStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("B")]
        public string BookPrefix {
            get {
                return ((string)(this["BookPrefix"]));
            }
            set {
                this["BookPrefix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string BookSuffix {
            get {
                return ((string)(this["BookSuffix"]));
            }
            set {
                this["BookSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public int BookMinCharLen {
            get {
                return ((int)(this["BookMinCharLen"]));
            }
            set {
                this["BookMinCharLen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string scanCOM {
            get {
                return ((string)(this["scanCOM"]));
            }
            set {
                this["scanCOM"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BookUseISBN {
            get {
                return ((bool)(this["BookUseISBN"]));
            }
            set {
                this["BookUseISBN"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("400000000000")]
        public long BoardStart {
            get {
                return ((long)(this["BoardStart"]));
            }
            set {
                this["BoardStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("G")]
        public string BoardPrefix {
            get {
                return ((string)(this["BoardPrefix"]));
            }
            set {
                this["BoardPrefix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string BoardSuffix {
            get {
                return ((string)(this["BoardSuffix"]));
            }
            set {
                this["BoardSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public int BoardMinCharLen {
            get {
                return ((int)(this["BoardMinCharLen"]));
            }
            set {
                this["BoardMinCharLen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastGUserCred {
            get {
                return ((string)(this["LastGUserCred"]));
            }
            set {
                this["LastGUserCred"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string BookFolder {
            get {
                return ((string)(this["BookFolder"]));
            }
            set {
                this["BookFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GameFolder {
            get {
                return ((string)(this["GameFolder"]));
            }
            set {
                this["GameFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ObjectFolder {
            get {
                return ((string)(this["ObjectFolder"]));
            }
            set {
                this["ObjectFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowContacts {
            get {
                return ((bool)(this["ShowContacts"]));
            }
            set {
                this["ShowContacts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowLendings {
            get {
                return ((bool)(this["ShowLendings"]));
            }
            set {
                this["ShowLendings"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowBorrowings {
            get {
                return ((bool)(this["ShowBorrowings"]));
            }
            set {
                this["ShowBorrowings"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowItems {
            get {
                return ((bool)(this["ShowItems"]));
            }
            set {
                this["ShowItems"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowBooks {
            get {
                return ((bool)(this["ShowBooks"]));
            }
            set {
                this["ShowBooks"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowBoardGames {
            get {
                return ((bool)(this["ShowBoardGames"]));
            }
            set {
                this["ShowBoardGames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowGames {
            get {
                return ((bool)(this["ShowGames"]));
            }
            set {
                this["ShowGames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowRecipes {
            get {
                return ((bool)(this["ShowRecipes"]));
            }
            set {
                this["ShowRecipes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowObjects {
            get {
                return ((bool)(this["ShowObjects"]));
            }
            set {
                this["ShowObjects"] = value;
            }
        }
    }
}
