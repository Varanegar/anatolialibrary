using System.Data.Entity;

namespace TrackingMap.Service.DBManagement
{
    public class MapDBInitializer : CreateDatabaseIfNotExists<MapContext>
    {
        private readonly string[] _customCommands;
        public MapDBInitializer(string[] customCommands)
        {
            _customCommands = customCommands;
        }
        protected override void Seed(MapContext context)
        {
            if (_customCommands != null && _customCommands.Length > 0)
            {
                foreach (var command in _customCommands)
                    context.Database.ExecuteSqlCommand(command);
            }
            base.Seed(context);
        }
    }
}
