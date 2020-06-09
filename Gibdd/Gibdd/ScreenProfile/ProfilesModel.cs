using System;
using System.Collections.Generic;
using System.Text;

namespace Gibdd.ScreenProfile
{
    class ProfilesModel : IProfilesModel
    {
        public string NameProfile { get; set; }

        public void OnClick(Profile profile)
        {
            NameProfile = profile.Name;
        }
    }
}
