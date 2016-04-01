#region Copyright (C) 2007-2015 Team MediaPortal

/*
    Copyright (C) 2007-2015 Team MediaPortal
    http://www.team-mediaportal.com

    This file is part of MediaPortal 2

    MediaPortal 2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    MediaPortal 2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MediaPortal 2. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace MediaPortal.Extensions.OnlineLibraries.Libraries.OmDbV1.Data
{
  //{
  //  "Title": "Game of Thrones",
  //  "Season": "1",
  //  "Episodes": [
  //    {
  //      "Title": "Winter Is Coming",
  //      "Released": "2011-04-17",
  //      "Episode": "1",
  //      "imdbRating": "8.9",
  //      "imdbID": "tt1480055"
  //    }
  //  ],
  //  "Response": "True"
  //}
  [DataContract]
  public class Season : BaseResponse
  {
    [DataMember(Name = "Title")]
    public string Title { get; set; }

    [DataMember(Name = "Season")]
    public int SeasonNumber { get; set; }

    [DataMember(Name = "Episodes")]
    public List<SeasonEpisode> Episodes { get; set; }

    [DataMember(Name = "Response")]
    public bool ResponseValid { get; set; }

    public void InitEpisodes()
    {
      InitProperties();

      foreach(SeasonEpisode epsiode in Episodes) epsiode.InitProperties();
    }
  }
}