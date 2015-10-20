﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer;
using HttpServer.Exceptions;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Plugins.MP2Extended.ResourceAccess.WSS.json.Profiles.BaseClasses;
using MediaPortal.Plugins.MP2Extended.ResourceAccess.WSS.Profiles;
using MediaPortal.Plugins.MP2Extended.WSS.Profiles;
using MediaPortal.Plugins.MP2Extended.WSS.StreamInfo;

namespace MediaPortal.Plugins.MP2Extended.ResourceAccess.WSS.json.Profiles
{
  internal class GetTranscoderProfilesForTarget : BaseTranscoderProfile, IRequestMicroModuleHandler
  {
    public dynamic Process(IHttpRequest request)
    {
      HttpParam httpParam = request.Param;
      string target = httpParam["target"].Value;
      if (target == null)
        throw new BadRequestException("GetTranscoderProfilesForTarget: target is null");


      return ProfileManager.Profiles.Where(x => x.Value.Targets.Contains(target)).Select(profile => TranscoderProfile(profile)).ToList();
    }

    internal static ILogger Logger
    {
      get { return ServiceRegistration.Get<ILogger>(); }
    }
  }
}
