﻿#pragma checksum "..\..\..\Pages\RatesAdd.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A6BD896150D6B73B0B6A1571A365624137147FD2869A6CA63B60CA2EC0B0510E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using GruzChelb.Pages;
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


namespace GruzChelb.Pages {
    
    
    /// <summary>
    /// RatesAdd
    /// </summary>
    public partial class RatesAdd : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\Pages\RatesAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock blockNamePage;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Pages\RatesAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputRateName;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Pages\RatesAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox choseTypeCar;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\RatesAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputPriceKm;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\RatesAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\RatesAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
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
            System.Uri resourceLocater = new System.Uri("/GruzChelb;component/pages/ratesadd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\RatesAdd.xaml"
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
            this.blockNamePage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.inputRateName = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\Pages\RatesAdd.xaml"
            this.inputRateName.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.InputNamePreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.choseTypeCar = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.inputPriceKm = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\Pages\RatesAdd.xaml"
            this.inputPriceKm.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.InputPricePreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Pages\RatesAdd.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.BtnSaveClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Pages\RatesAdd.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.BtnBackClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

