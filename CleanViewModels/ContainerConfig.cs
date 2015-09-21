using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CleanViewModels.PodcastEpisode.Wizard;
using CleanViewModels.PodcastEpisode.Repositories;
using CleanViewModels.PodcastEpisode.Services;

namespace CleanViewModels
{
    class ContainerConfig
    {
        private IContainer _container;

        public ContainerConfig()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<GenreRepository>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<UploadService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<TitleViewModel>().AsSelf();
            builder.RegisterType<FileViewModel>().AsSelf();
            builder.RegisterType<UrlViewModel>().AsSelf();
            builder.RegisterType<ReviewViewModel>().AsSelf();
            builder.RegisterType<WizardViewModel>().AsSelf();

            _container = builder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
