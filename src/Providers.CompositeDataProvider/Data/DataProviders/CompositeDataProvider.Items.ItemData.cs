﻿namespace Sitecore.Data.DataProviders
{
  using Sitecore.Collections;
  using Sitecore.Data.Items;
  using Sitecore.Extensions.Enumerable;
  using Sitecore.Globalization;

  public partial class CompositeDataProvider
  {
    /* Items.ItemData part of DataProvider */

    public override FieldList GetItemFields(ItemDefinition itemDefinition, VersionUri versionUri, CallContext context)
    {
      return HeadProvider.GetItemFields(itemDefinition, versionUri, context) 
        ?? ReadOnlyProviders.FirstNotNull(x => x.GetItemFields(itemDefinition, versionUri));
    }

    public override VersionUriList GetItemVersions(ItemDefinition itemDefinition, CallContext context)
    {
      var headList = HeadProvider.GetItemVersions(itemDefinition, context);

      return 
        headList != null && headList.Count > 0 
        ? headList 
        : ReadOnlyProviders.FirstNotNull(x => x.GetItemVersions(itemDefinition));
    }

    public override bool SaveItem(ItemDefinition itemDefinition, ItemChanges changes, CallContext context)
    {
      return HeadProvider.SaveItem(itemDefinition, changes, context);
    }

    public override int AddVersion(ItemDefinition itemDefinition, VersionUri baseVersion, CallContext context)
    {
      return HeadProvider.AddVersion(itemDefinition, baseVersion, context);
    }

    public override bool RemoveVersion(ItemDefinition itemDefinition, VersionUri version, CallContext context)
    {
      return HeadProvider.RemoveVersion(itemDefinition, version, context);
    }

    public override bool RemoveVersions(ItemDefinition itemDefinition, Language language, CallContext context)
    {
      return HeadProvider.RemoveVersions(itemDefinition, language, context);
    }

    public override bool RemoveVersions(ItemDefinition itemDefinition, Language language, bool removeSharedData, CallContext context)
    {
      return HeadProvider.RemoveVersions(itemDefinition, language, removeSharedData, context);
    }
  }
}