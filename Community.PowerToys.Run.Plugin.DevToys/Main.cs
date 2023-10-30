// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Wox.Infrastructure;
using Wox.Plugin;

namespace Community.PowerToys.Run.Plugin.DevToys
{
    public class Main : IPlugin
    {
        public static string PluginID => "9D45F332530A4B1FACA8A703773119DA";

        private readonly DevToysProvider _devToysProvider;
        private bool _devToysInstalled;

        public string Name => "DevToys";

        public string Description => "Open DevToys utilities.";

        public Main()
        {
            _devToysProvider = new DevToysProvider();
        }

        public void Init(PluginInitContext context)
        {
            _devToysInstalled = _devToysProvider.FindDevToys();
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();

            if (!_devToysInstalled)
            {
                return results;
            }

            foreach (var u in _devToysProvider.Utilities)
            {
                var score = StringMatcher.FuzzySearch(query.Search, u.Name);
                if (string.IsNullOrWhiteSpace(query.Search) || score.Score > 0)
                {
                    results.Add(new Result
                    {
                        Title = u.Name,
                        SubTitle = "DevToys Utility",
                        Score = score.Score,
                        TitleHighlightData = score.MatchData,
                        Icon = () => _devToysProvider.Logo,
                        Action = _ =>
                        {
                            Helper.OpenInShell($"devtoys:?tool={u.Protocol}");
                            return true;
                        },
                    });
                }
            }

            return results.OrderBy(r => r.Title).ToList();
        }
    }
}
