using System;
using System.Collections.Generic;
using System.Text;

namespace Gibdd.ScreenProfile
{
    public interface IProfilesModel
    {
        void OnClick(Profile profile);

        string  NameProfile { get; set; }
    }
}
