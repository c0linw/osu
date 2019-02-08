// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Replays;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModExpand<T> : ModExpand, IApplicableToRulesetContainer<T>
        where T : HitObject
    {
        protected virtual Score CreateReplayScore(Beatmap<T> beatmap) => new Score { Replay = new Replay() };

        public override bool HasImplementation => GetType().GenericTypeArguments.Length == 0;

        public virtual void ApplyToRulesetContainer(RulesetContainer<T> rulesetContainer) => rulesetContainer.SetReplayScore(CreateReplayScore(rulesetContainer.Beatmap));
    }

    public abstract class ModExpand : Mod
    {
        public override string Name => "Expand";
        public override string Acronym => "EX";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_auto;
        public override ModType Type => ModType.Fun;
        public override string Description => "Circles expand into the approach circle.";
        public override double ScoreMultiplier => 1;
    }
}
