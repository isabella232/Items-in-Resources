﻿namespace IR.Tests
{
  using System;
  using IR.Data;
  using IR.Data.DataAccess;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class ItemPathResolverTests
  {
    [TestMethod]
    public void TesT1()
    {
      var root = new ItemInfo { ID = ItemIDs.RootItemID, Name = "Sitecore" };
      var namesCache = new ChildrenDataSet(new ItemInfoSet(root));

      // act
      Guid id;
      var result = ItemPathResolver.TryResolvePath("/home", namesCache, out id);

      Assert.IsFalse(result);       
      Assert.AreEqual(Guid.Empty, id);
    }

    [TestMethod]
    public void TesT2()
    {
      var root = new ItemInfo { ID = ItemIDs.RootItemID, Name = "Sitecore" };
      var namesCache = new ChildrenDataSet(new ItemInfoSet(root));

      // act
      Guid id;
      var result = ItemPathResolver.TryResolvePath("/sitecore", namesCache, out id);

      Assert.IsTrue(result);
      Assert.AreEqual(root.ID, id);
    }

    [TestMethod]
    public void TesT22()
    {
      var root = new ItemInfo { ID = ItemIDs.RootItemID, Name = "Sitecore" };
      var namesCache = new ChildrenDataSet(new ItemInfoSet(root));

      // act
      Guid id;
      var result = ItemPathResolver.TryResolvePath("/sitecore/", namesCache, out id);

      Assert.IsTrue(result);
      Assert.AreEqual(root.ID, id);
    }

    [TestMethod]
    public void TesT3()
    {
      var root = new ItemInfo { ID = ItemIDs.RootItemID, Name = "Sitecore" };
      var content = new ItemInfo { ID = Guid.NewGuid(), Name = "content", ParentID = root.ID };
      var namesCache = new ChildrenDataSet(new ItemInfoSet(root, content));

      // act
      Guid id;
      var result = ItemPathResolver.TryResolvePath("/sitecore/content", namesCache, out id);

      Assert.IsTrue(result);
      Assert.AreEqual(content.ID, id);
    }

    [TestMethod]
    public void TesT4()
    {
      var root = new ItemInfo { ID = ItemIDs.RootItemID, Name = "Sitecore" };
      var content = new ItemInfo { ID = Guid.NewGuid(), Name = "content", ParentID = root.ID };
      var namesCache = new ChildrenDataSet(new ItemInfoSet(root, content));

      // act
      Guid id;
      var result = ItemPathResolver.TryResolvePath("/sitecore/system", namesCache, out id);

      Assert.IsFalse(result);
      Assert.AreEqual(root.ID, id);
    }       

    [TestMethod]
    public void TesT5()
    {
      var root = new ItemInfo { ID = ItemIDs.RootItemID, Name = "Sitecore" };
      var content1 = new ItemInfo { ID = Guid.NewGuid(), Name = "content", ParentID = root.ID };
      var content2 = new ItemInfo { ID = Guid.NewGuid(), Name = "content", ParentID = root.ID };
      var home = new ItemInfo { ID = Guid.NewGuid(), Name = "Home", ParentID = content2.ID };
      var namesCache = new ChildrenDataSet(new ItemInfoSet(root, content1, content2, home));

      // act
      Guid id;
      var result = ItemPathResolver.TryResolvePath("/sitecore/content/home", namesCache, out id);

      Assert.IsTrue(result);
      Assert.AreEqual(home.ID, id);
    }    
  }
}
