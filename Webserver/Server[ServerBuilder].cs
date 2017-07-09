﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using RequestDict = System.Collections.Immutable.ImmutableSortedDictionary<System.Text.RegularExpressions.Regex, System.Action<Jambox.Web.Request>>;
using RequestDictBuilder = System.Collections.Immutable.ImmutableSortedDictionary<System.Text.RegularExpressions.Regex, System.Action<Jambox.Web.Request>>.Builder;
namespace Jambox.Web
{
    public partial class Server
    {
        public class ServerBuilder
        {
            Server ws;
            RequestDictBuilder getRq;
            RequestDictBuilder putRq;
            RequestDictBuilder postRq;
            RequestDictBuilder delRq;
            RegexOptions caseSensitive;
            public ServerBuilder(RegexOptions caseSensitivity)
            {
                ws = new Server();
                getRq = System.Collections.Immutable.ImmutableSortedDictionary.CreateBuilder<Regex, Action<Request>>();
                putRq = System.Collections.Immutable.ImmutableSortedDictionary.CreateBuilder<Regex, Action<Request>>();
                postRq = System.Collections.Immutable.ImmutableSortedDictionary.CreateBuilder<Regex, Action<Request>>();
                delRq = System.Collections.Immutable.ImmutableSortedDictionary.CreateBuilder<Regex, Action<Request>>();
                RegexOptions caseSensitive = caseSensitivity;
            }
            public ServerBuilder GET(string pattern, Action<Request> action)
            {
                getRq.Add(new Regex(pattern, RegexOptions.Compiled | caseSensitive), action);
                return this;
            }
            public ServerBuilder GET(string pattern, Action<Request> action, bool caseSensitive)
            {
                getRq.Add(new Regex(pattern, 
                    RegexOptions.Compiled | (caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None)), action);
                return this;
            }
            public ServerBuilder POST(string pattern, Action<Request> action)
            {
                postRq.Add(new Regex(pattern, RegexOptions.Compiled | caseSensitive), action);
                return this;
            }
            public ServerBuilder POST(string pattern, Action<Request> action, bool caseSensitive)
            {
                postRq.Add(new Regex(pattern,
                    RegexOptions.Compiled | (caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None)), action);
                return this;
            }
            public ServerBuilder PUT(string pattern, Action<Request> action)
            {
                putRq.Add(new Regex(pattern, RegexOptions.Compiled | caseSensitive), action);
                return this;
            }
            public ServerBuilder PUT(string pattern, Action<Request> action, bool caseSensitive)
            {
                putRq.Add(new Regex(pattern,
                    RegexOptions.Compiled | (caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None)), action);
                return this;
            }
            public ServerBuilder DELETE(string pattern, Action<Request> action)
            {
                delRq.Add(new Regex(pattern, RegexOptions.Compiled | caseSensitive), action);
                return this;
            }
            public ServerBuilder DELETE(string pattern, Action<Request> action, bool caseSensitive)
            {
                delRq.Add(new Regex(pattern,
                    RegexOptions.Compiled | (caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None)), action);
                return this;
            }
            public Server Build()
            {
                ws.getRequestMap = getRq.ToImmutable();
                ws.postRequestMap = postRq.ToImmutable();
                ws.putRequestMap = putRq.ToImmutable();
                ws.deleteMapping = delRq.ToImmutable();
                return ws;
            }
        }
    }
}
