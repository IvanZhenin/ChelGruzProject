﻿#pragma checksum "..\..\..\Pages\OrdersList.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "70A3C27BE1BB90A6C79AA4575CB7F10677BF695D090486A6D0F92CB3C8BBBF71"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using GruzChelb;
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


namespace GruzChelb {
    
    
    /// <summary>
    /// OrdersList
    /// </summary>
    public partial class OrdersList : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 27 "..\..\..\Pages\OrdersList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxSrchOrders;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\OrdersList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbBoxSrchRates;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\OrdersList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbBoxSrchStatus;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Pages\OrdersList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewOrders;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\Pages\OrdersList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\Pages\OrdersList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDel;
        
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
            System.Uri resourceLocater = new System.Uri("/GruzChelb;component/pages/orderslist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\OrdersList.xaml"
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
            
            #line 9 "..\..\..\Pages\OrdersList.xaml"
            ((GruzChelb.OrdersList)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.PageIsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtBoxSrchOrders = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\Pages\OrdersList.xaml"
            this.txtBoxSrchOrders.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtBoxTargetTextChanged);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\Pages\OrdersList.xaml"
            this.txtBoxSrchOrders.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TxtBoxSrchOrdersPreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmbBoxSrchRates = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\Pages\OrdersList.xaml"
            this.cmbBoxSrchRates.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbBoxSrchRatesSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbBoxSrchStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\..\Pages\OrdersList.xaml"
            this.cmbBoxSrchStatus.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbBoxSrchStatusSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 40 "..\..\..\Pages\OrdersList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnGoMainPageClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.listViewOrders = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 180 "..\..\..\Pages\OrdersList.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.BtnAddClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnDel = ((System.Windows.Controls.Button)(target));
            
            #line 183 "..\..\..\Pages\OrdersList.xaml"
            this.btnDel.Click += new System.Windows.RoutedEventHandler(this.BtnDelClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 7:
            
            #line 166 "..\..\..\Pages\OrdersList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnProdListClick);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 168 "..\..\..\Pages\OrdersList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnRedClick);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

