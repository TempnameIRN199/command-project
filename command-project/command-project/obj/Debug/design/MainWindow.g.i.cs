﻿#pragma checksum "..\..\..\design\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9D7C018623E62D3313E1363494C7234A889C4124BC7E76D301268454442E5053"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using command_project;


namespace command_project {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControlMain;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSign;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbLoginSign;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pbPassSign;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bCantSign;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSingGuest;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEmailReg;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bRegistration;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbCheckReg;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\design\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid bExit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/command-project;component/design/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\design\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tabControlMain = ((System.Windows.Controls.TabControl)(target));
            return;
            case 2:
            this.bSign = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\design\MainWindow.xaml"
            this.bSign.Click += new System.Windows.RoutedEventHandler(this.bSign_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbLoginSign = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.pbPassSign = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.bCantSign = ((System.Windows.Controls.Button)(target));
            
            #line 137 "..\..\..\design\MainWindow.xaml"
            this.bCantSign.Click += new System.Windows.RoutedEventHandler(this.bCantSign_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bSingGuest = ((System.Windows.Controls.Button)(target));
            
            #line 162 "..\..\..\design\MainWindow.xaml"
            this.bSingGuest.Click += new System.Windows.RoutedEventHandler(this.bSingAdm_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbEmailReg = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.bRegistration = ((System.Windows.Controls.Button)(target));
            
            #line 171 "..\..\..\design\MainWindow.xaml"
            this.bRegistration.Click += new System.Windows.RoutedEventHandler(this.bRegistration_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cbCheckReg = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.bExit = ((System.Windows.Controls.Grid)(target));
            
            #line 178 "..\..\..\design\MainWindow.xaml"
            this.bExit.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.bExit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

