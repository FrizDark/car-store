﻿#pragma checksum "..\..\..\..\View\Profile\AdminUserEditToolView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7BD32C098CD30A86B0DC82A6AA4B09F31FDAF72EB326B9C7132AB1FAB1D8658C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Carstore.Properties;
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


namespace Carstore.View.Profile {
    
    
    /// <summary>
    /// AdminUserEditToolView
    /// </summary>
    public partial class AdminUserEditToolView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UserButton;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModeratorButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Carstore;component/view/profile/adminuseredittoolview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
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
            
            #line 9 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            ((Carstore.View.Profile.AdminUserEditToolView)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.UserControl_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.UserButton = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            this.UserButton.Click += new System.Windows.RoutedEventHandler(this.UserButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ModeratorButton = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            this.ModeratorButton.Click += new System.Windows.RoutedEventHandler(this.ModeratorButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 70 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            this.NameBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dg = ((System.Windows.Controls.DataGrid)(target));
            
            #line 80 "..\..\..\..\View\Profile\AdminUserEditToolView.xaml"
            this.dg.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

