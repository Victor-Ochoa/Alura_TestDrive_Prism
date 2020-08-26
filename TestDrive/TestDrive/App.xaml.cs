using Prism;
using Prism.Ioc;
using TestDrive.ViewModels;
using TestDrive.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestDrive.Interfaces;
using TestDrive.Services;
using LiteDB;
using System.IO;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestDrive
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("Login");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Listagem, ListagemViewModel>();
            containerRegistry.RegisterForNavigation<Detalhe, DetalheViewModel>();
            containerRegistry.RegisterForNavigation<Views.Agendamento, AgendamentoViewModel>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MasterDetail, MasterDetailViewModel>();
            containerRegistry.RegisterForNavigation<Master, MasterViewModel>();
            containerRegistry.RegisterForNavigation<TabEditar, TabEditarViewModel>();
            containerRegistry.RegisterForNavigation<TabUsuario, TabUsuarioViewModel>();
            containerRegistry.RegisterForNavigation<MeusAgendamentos, MeusAgendamentosViewModel>();

            containerRegistry.Register<IAgendamentoService, AgendamentoService>();
            containerRegistry.Register<IVeiculoService, VeiculoService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.RegisterInstance<ILiteDatabase>(new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "TestDriveDb.db")));

            containerRegistry.RegisterSingleton<IMemoryService, MemoryService>();
        }
    }
}
