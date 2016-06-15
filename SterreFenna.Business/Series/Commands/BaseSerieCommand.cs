using SterreFenna.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class BaseSerieCommand
    {
        private readonly SFContext _context;
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;

        public BaseSerieCommand(AddItemsToSerieCommand addItemsToSerieCommand, SFContext context)
        {
            _addItemsToSerieCommand = addItemsToSerieCommand;
            _context = context;
        }

        public string SerieName { get; set; }

        public string ProjectName { get; set; }

        public int? ProjectId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public List<UploadedSerieItem> SerieItems { get; set; }

        public void StoreImages(Serie serie)
        {
            _addItemsToSerieCommand.SerieItems = SerieItems;
            _addItemsToSerieCommand.Handle(serie);
        }

        public int? GetProjectId()
        {
            var project = CreateProject();

            if (ProjectId.HasValue)
                return ProjectId.Value;

            if (project != null)
                return project.Id;

            return null;
        }

        private Project CreateProject()
        {
            if (ProjectName.HasValue())
            {
                var project = new Project
                {
                    Name = ProjectName,
                };

                _context.Projects.Add(project);
                _context.SaveChanges();

                return project;
            }

            return null;
        }
    }
}
