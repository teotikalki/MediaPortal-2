﻿#region Copyright (C) 2007-2012 Team MediaPortal

/*
    Copyright (C) 2007-2012 Team MediaPortal
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

using System.Collections.Generic;
using System.Text.RegularExpressions;
using MediaPortal.Common.MediaManagement.Helpers;

namespace MediaPortal.Extensions.MetadataExtractors.VideoMetadataExtractor.NameMatchers
{
  public class SeriesMatcher
  {
    private const string GROUP_SERIES = "series";
    private const string GROUP_SEASONNUM = "seasonnum";
    private const string GROUP_EPISODENUM = "episodenum";
    private const string GROUP_EPISODE = "episode";

    protected List<Regex> _matchers = new List<Regex>
                               {
                                 // Episode scanner recommendations for recordings
                                 new Regex(@"(?<series>[^\\]+) - \((?<episode>.*)\) S(?<seasonnum>[0-9]+?)E(?<episodenum>[0-9]+?)", RegexOptions.IgnoreCase),
                                 new Regex(@"(?<series>[^\\]+)\W(?<seasonnum>\d+)x((?<episodenum>\d+)_?)+ - (?<episode>.*)\.", RegexOptions.IgnoreCase),
                                 new Regex(@"(?<series>[^\\]+)\WS(?<seasonnum>\d+)E((?<episodenum>\d+)_?)+ - (?<episode>.*)\.", RegexOptions.IgnoreCase),
                               };

    public bool MatchSeries(string folderOrFileName, out SeriesInfo seriesInfo)
    {
      foreach (Regex matcher in _matchers)
      {
        Match ma = matcher.Match(folderOrFileName);
        seriesInfo = ParseSeries(ma);
        if (seriesInfo.IsCompleteMatch)
          return true;
      }
      seriesInfo = null;
      return false;
    }

    static SeriesInfo ParseSeries(Match ma)
    {
      SeriesInfo info = new SeriesInfo();
      if (ma.Groups[GROUP_SERIES].Length > 0)
        info.Series = ma.Groups[GROUP_SERIES].Value;
      if (ma.Groups[GROUP_EPISODE].Length > 0)
        info.Episode = ma.Groups[GROUP_EPISODE].Value;
      if (ma.Groups[GROUP_SEASONNUM].Length > 0)
        int.TryParse(ma.Groups[GROUP_SEASONNUM].Value, out info.SeasonNumber);

      // There can be multipe episode numbers in one file
      Group grpEpisodeNums = ma.Groups[GROUP_EPISODENUM];
      if (grpEpisodeNums.Length > 0)
      {
        foreach (Capture capture in grpEpisodeNums.Captures)
        {
          int episodeNum;
          if (int.TryParse(capture.Value, out episodeNum))
            info.EpisodeNumbers.Add(episodeNum);
        }
      }
      return info;
    }
  }
}
