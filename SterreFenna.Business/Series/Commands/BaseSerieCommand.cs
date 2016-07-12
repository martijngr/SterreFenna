using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class BaseSerieCommand
    {
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;

        public BaseSerieCommand(AddItemsToSerieCommand addItemsToSerieCommand)
        {
            _addItemsToSerieCommand = addItemsToSerieCommand;
        }

        public string SerieName { get; set; }

        public string ProjectName { get; set; }

        public int ProjectId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public List<UploadedSerieItem> SerieItems { get; set; }

        public void StoreImages(Serie serie)
        {
            _addItemsToSerieCommand.SerieItems = SerieItems;
            _addItemsToSerieCommand.Handle(serie);
        }
    }
}
