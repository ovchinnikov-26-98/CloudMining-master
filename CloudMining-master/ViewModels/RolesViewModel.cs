using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories;
using CloudMining.Repositories.Base;
using System.Collections.ObjectModel;

namespace CloudMining.ViewModels
{
	public class RolesViewModel : BaseViewModel
	{
		private readonly IRepository<Role> _RolesRepository;

		#region Constructor
		public RolesViewModel()
		{
			this._RolesRepository = new RolesRepository(new BaseDataContext());

			Roles = new ObservableCollection<Role>(_RolesRepository.GetAll());
		}
		#endregion

		#region Properties
		private ObservableCollection<Role> _Roles;
		public ObservableCollection<Role> Roles
		{
			get => _Roles;
			set => Set(ref _Roles, value);
		}
		#endregion
	}
}
