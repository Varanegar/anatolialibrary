using System;

namespace Anatoli.ViewModels.AppModels
{
    [Serializable]
    public class ApplicationViewModel //: BaseViewModel
    {
        public ApplicationViewModel()
        {
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
