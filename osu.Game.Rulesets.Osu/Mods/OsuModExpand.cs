// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables.Pieces;
using osuTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModExpand : Mod, IApplicableToDrawableHitObjects
    {
        public override string Name => "Expand";
        public override string Acronym => "EX";
        public override FontAwesome Icon => FontAwesome.fa_certificate;
        public override ModType Type => ModType.Fun;
        public override string Description => "In Mother Russia, circle approach you.";
        public override double ScoreMultiplier => 1;
        public override Type[] IncompatibleMods => new[] { typeof(OsuModHidden), typeof(OsuModWiggle), typeof(OsuModTransform) };

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var drawable in drawables)
                drawable.ApplyCustomUpdateState += drawableOnApplyCustomUpdateState;
        }

        private void drawableOnApplyCustomUpdateState(DrawableHitObject drawable, ArmedState state)
        {
            var hitObject = (OsuHitObject)drawable.HitObject;
            Vector2 origin = drawable.Position;

            switch(drawable)
            {
                case DrawableHitCircle circle:
                    using (circle.BeginAbsoluteSequence(hitObject.StartTime - hitObject.TimePreempt, true))
                    {
                        circle.Alpha = 0;
                        circle.Scale = new Vector2(0.1f);
                        circle.ScaleTo(hitObject.Scale, hitObject.TimePreempt * 0.75, Easing.In);
                        circle.FadeIn(Math.Min(hitObject.TimeFadeIn * 2, hitObject.TimePreempt));
                    }
                    using (circle.ApproachCircle.BeginAbsoluteSequence(hitObject.StartTime - hitObject.TimePreempt, true))
                    {
                        circle.ApproachCircle.Alpha = 0;
                        circle.ApproachCircle.Scale = new Vector2(20);
                        circle.ApproachCircle.FadeIn(Math.Min(hitObject.TimeFadeIn * 2, hitObject.TimePreempt));
                        circle.ApproachCircle.ScaleTo(1.1f, hitObject.TimePreempt); ;
                    }
                        break;
            }
        }
    }
}
