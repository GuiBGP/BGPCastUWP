using BGPCastUWP.Interface.UWP.Views;
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

        public Type Destination { get; set; }
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

        public static List<NavMenuItem> GetList()
        {
                return  new List<NavMenuItem>(
                        new[]
                        {
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/portalcafebrasil.jpeg",
                                Name ="Café Brasil Podcast",
                                News ="399 NOVO",
                                Quantity ="55"
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/central3.jpeg",
                                Name ="Central3 Podcasts - Xadrez Verbal",
                                News ="19 NOVO",
                                Quantity ="2"
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/jovemnerd.jpeg",
                                Name ="NerdCast",
                                News ="534 NOVO",
                                Quantity ="2"
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/pln.jpeg",
                                Name ="Pauta Livre News",
                                News ="213 NOVO",
                                Quantity ="2"
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/peladananet.jpeg",
                                Name ="Pelada na Net Podcast",
                                News ="201 NOVO",
                                Quantity =""
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/redegeek.jpeg",
                                Name ="Rede Geek - Ultrageek",
                                News ="95 NOVO",
                                Quantity ="2"
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/radiofobia.jpeg",
                                Name ="Rádiofobia Podcasts",
                                News ="286 NOVO",
                                Quantity ="2"
                            },
                            new NavMenuItem()
                            {
                                Destination = typeof(WhatPlayPage),
                                Logo = "/Assets/PodcastSymbol/deviante.jpeg",
                                Name ="Scicast",
                                News ="32 NOVO",
                                Quantity ="5"
                            },
                        });
        }

    }
}
