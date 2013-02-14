using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.Interfaces;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cirrious.MvvmCross.Binding
{
	public class MvxBindablePickerViewDataSource : MvxBaseBindablePickerViewDataSource
	{
		private IList _itemsSource;
		
		protected MvxBindablePickerViewDataSource(UIPickerView pickerView)
			: base(pickerView)
		{
		}
		
		[MvxSetToNullAfterBinding]
		public virtual IList ItemsSource
		{
			get { return _itemsSource; }
			set
			{
				if (_itemsSource == value)
					return;
				
				var collectionChanged = _itemsSource as INotifyCollectionChanged;
				if (collectionChanged != null)
					collectionChanged.CollectionChanged -= CollectionChangedOnCollectionChanged;
				_itemsSource = value;
				collectionChanged = _itemsSource as INotifyCollectionChanged;
				if (collectionChanged != null)
					collectionChanged.CollectionChanged += CollectionChangedOnCollectionChanged;
				ReloadTableData ();
			}
		}
		
//		protected override object GetItemAt(int index)
//		{
//			if (ItemsSource == null)
//				return null;
//			
//			return ItemsSource[index];
//		}
		
		protected virtual void CollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			ReloadTableData ();
		}
		
		public override int GetRowsInComponent (UIPickerView pickerView, int component)
		{
			if (ItemsSource == null)
				return 0;
			
			return ItemsSource.Count;
		}

		public override int GetComponentCount (UIPickerView pickerView)
		{
			return 1;
		}
	}
}

