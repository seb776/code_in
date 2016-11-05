﻿using code_in.Presenters.Nodal.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    /// <summary>
    /// Logique d'interaction pour TileContainer.xaml
    /// </summary>
    public partial class TileContainer : UserControl, ITileContainer, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public TileContainer(ResourceDictionary themeResDict, INodalView nodalView)
        {
            this.NodalView = nodalView;
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }
        public TileContainer() :
            this(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        #region This
        public bool IsExpanded
        {
            get
            {
                return this.IsEnabled;
            }
            set
            {
                this.IsEnabled = value;
                if (value)
                {
                    this.Visibility = System.Windows.Visibility.Visible;


                }
                else
                    this.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        #endregion This
        #region ITileContainer
        public void UpdateDisplayedInfosFromPresenter()
        {
            foreach (var v in TileStackPannel.Children)
            {
                (v as BaseTile).UpdateDisplayedInfosFromPresenter();
            }
        }
        public T CreateAndAddTile<T>(INodePresenter nodePresenter) where T : BaseTile
        {
            T tile = (T)Activator.CreateInstance(typeof(T), _themeResourceDictionary, this.NodalView);

            tile.SetParentView(this);
            tile.SetPresenter(nodePresenter);
            this.AddTile(tile);
            return tile;
        }


        public void AddTile<T>(T tile, int index = -1) where T : BaseTile
        {
            dynamic uiTile = tile;
            if (index < 0)
                this.TileStackPannel.Children.Add(uiTile);
            else
                this.TileStackPannel.Children.Insert(index + 1, uiTile);
        }

        public void RemoveTile(BaseTile tile)
        {
            throw new NotImplementedException();
        }
        #endregion ITileContainer
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual
        #region INodalViewElement
        public INodalView NodalView
        {
            get;
            set;
        }
        #endregion INodalViewElement
        #region IContainerDragNDrop
        public void Drag(EDragMode dragMode)
        {
            throw new NotImplementedException();
        }

        public void UpdateDragInfos(Point mousePosToMainGrid)
        {
            throw new NotImplementedException();
        }

        public new void Drop(IEnumerable<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }

        public bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }

        #endregion IContainerDragNDrop

        #region IDragNDropItem
        public void SelectHighLight(bool highlighetd)
        {
            throw new NotImplementedException();
        }

        public void MustBeRemovedFromContext()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromContext()
        {
            throw new NotImplementedException();
        }

        public void SetParentView(IContainerDragNDrop vc)
        {
            throw new NotImplementedException();
        }

        public IContainerDragNDrop GetParentView()
        {
            throw new NotImplementedException();
        }
        #endregion IDragNDropItem
    }
}
