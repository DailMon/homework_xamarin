

using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Gibdd
{
    public interface IGibddModel
    {
        ObservableCollection<MyImage> ImageItems { get; set; }
        Task TakePhoto();
        Task PickPhoto();
        void DeletePhoto(MyImage SelectedPhoto);
        bool IsPhotoAdd { get; set; }

    }
}
