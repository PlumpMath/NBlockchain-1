﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBlockchain.P2PPrototocol.AgentAPI;
using NBlockchain.P2PPrototocol.Network;
using NBlockchain.P2PPrototocol.NodeJSAPI;
using NBlockchain.P2PPrototocol.Repository;

namespace NBlockchain.P2PPrototocol.lUnitTest
{
  [TestClass]
  public class AgentServicesUnitTest
  {
    [TestMethod]
    public void ServiceResponseAcceptedTestMethod()
    {
      List<string> _log = new List<string>();
      using (AgentServices _services = new AgentServices(new TestRepository(), new NetworkAgent(), (x) => _log.Add(x)))
      {
        Assert.AreEqual<int>(1, _log.Count);
        using (HttpClient _client = new HttpClient())
        {
          _client.BaseAddress = new Uri("http://localhost:3001");
          HttpResponseMessage _message = _client.GetAsync("/peers").Result;
          Assert.IsNotNull(_message);
          Assert.AreEqual<HttpStatusCode>(HttpStatusCode.Accepted, _message.StatusCode);
        }
      }
      Assert.AreEqual<int>(2, _log.Count);
    }
    [TestMethod]
    public void ServiceResponseNotFoundTestMethod()
    {
      List<string> _log = new List<string>();
      using (AgentServices _services = new AgentServices(new TestRepository(), new NetworkAgent(), (x) => _log.Add(x)))
      {
        Assert.AreEqual<int>(1, _log.Count);
        using (HttpClient _client = new HttpClient())
        {
          _client.BaseAddress = new Uri("http://localhost:3001");
          HttpResponseMessage _message = _client.GetAsync("/wrong").Result;
          Assert.IsNotNull(_message);
          Assert.AreEqual<HttpStatusCode>(HttpStatusCode.NotFound, _message.StatusCode);
        }
      }
      Assert.AreEqual<int>(1, _log.Count);
    }
  }
  internal class NetworkAgent : INetworkAgentAPI
  {
    public List<JavaWebSocket> sockets { get; } = new List<JavaWebSocket>() { { new JavaWebSocket(new Uri("http://localhost:3001")) } };

    public void connectToPeers(Uri[] peer)
    {
      throw new NotImplementedException();
    }

    public void initP2PServer()
    {
      throw new NotImplementedException();
    }
  }

  internal class TestRepository : IRepositoryAgentInterface
  {
    public Block generateNextBlock(string data)
    {
      throw new NotImplementedException();
    }

    public string stringify()
    {
      throw new NotImplementedException();
    }
  }
}
