using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class EditSerieCommand : BaseSerieCommand
    {
        private readonly SFContext _context;

        public EditSerieCommand(AddItemsToSerieCommand addItemsToSerieCommand, SFContext context)
            : base(addItemsToSerieCommand, context)
        {
            _context = context;
        }

        public int SerieId { get; set; }

        public void Handle()
        {
            var serie = _context.Series.First(s => s.Id == SerieId);

            serie.Name = SerieName;
            serie.Published = PublicationDate;
            serie.ProjectId = GetProjectId();
            
            StoreImages(serie);
        }
    }
}