using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BGPCastUWP.Interface.UWP.Controls
{
    /// <summary>
    /// Data to represent an item in the nav menu.
    /// </summary>
    public class NavMenuItem
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
        public char SymbolAsChar
        {
            get
            {
                return (char)this.Symbol;
            }
        }

        public Type DestPage { get; set; }
        public object Arguments { get; set; }


        private string _logo;

        public string Logo
        {
            get
            {
               return (string.IsNullOrWhiteSpace(_logo) ? "/Assets/Square44x44Logo.scale-200.png" : _logo);
            }
            set
            {
                _logo = value;
            }
        }
        public string Name { get; set; }
        public string News { get; set; }
        public string Quantity { get; set; }




        /*
                    <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="48" />
                    <ColumnDefinition />
                    <ColumnDefinition />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition />
                    <RowDefinition />
                </Grid.RowDefinitions>
                <Image  x:Name="Logo"  Source="{x:Bind Logo}" Grid.Row="0" Grid.RowSpan="2" Grid.Column="0" />
                <TextBlock x:Name="Name" Grid.Row="0" Grid.Column="1" Text="{x:Bind Name}" />
                <TextBlock x:Name="News" Grid.Row="1" Grid.Column="1" Text="{x:Bind News}" />
                <TextBlock x:Name="Quantity" Grid.Row="0" Grid.Column="2" Grid.RowSpan="2" Text="{x:Bind Quantity}" />
            </Grid>
        
        
        */



    }
}
