﻿#pragma checksum "..\..\..\View\NotificationsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6D9E28124A2809DD9E380E916291775065E85C2E3E5E1B45872DAEF4168BC172"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Carstore.View;
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


namespace Carstore.View {
    
    
    /// <summary>
    /// NotificationsView
    /// </summary>
    public partial class NotificationsView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\View\NotificationsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllButton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\NotificationsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FromMeButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\View\NotificationsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ToMeButton;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\View\NotificationsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReloadButton;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\View\NotificationsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg;
        
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
            System.Uri resourceLocater = new System.Uri("/Carstore;component/view/notificationsview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\NotificationsView.xaml"
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
            this.AllButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\View\NotificationsView.xaml"
            this.AllButton.Click += new System.Windows.RoutedEventHandler(this.AllButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FromMeButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\View\NotificationsView.xaml"
            this.FromMeButton.Click += new System.Windows.RoutedEventHandler(this.FromMeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ToMeButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\View\NotificationsView.xaml"
            this.ToMeButton.Click += new System.Windows.RoutedEventHandler(this.ToMeButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ReloadButton = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\View\NotificationsView.xaml"
            this.ReloadButton.Click += new System.Windows.RoutedEventHandler(this.ReloadButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dg = ((System.Windows.Controls.DataGrid)(target));
            
            #line 66 "..\..\..\View\NotificationsView.xaml"
            this.dg.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
