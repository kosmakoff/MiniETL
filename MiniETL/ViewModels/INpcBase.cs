using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MiniETL.Annotations;

namespace MiniETL.ViewModels
{
	public abstract class INPCBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
		{
			var memberExpression = (MemberExpression) property.Body;
			OnPropertyChanged(memberExpression.Member.Name);
		}
	}
}
