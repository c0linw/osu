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
        public override Type[] IncompatibleMods => new[] { typeof(OsuModHidden), typeof(OsuModWiggle) };

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var drawable in drawables)
                drawable.ApplyCustomUpdateState += drawableOnApplyCustomUpdateState;
        }

        private void drawableOnApplyCustomUpdateState(DrawableHitObject drawable, ArmedState state)
        {
            if (!(drawable is DrawableOsuHitObject d))
                return;

            var h = d.HitObject;

            switch (drawable)
            {
                case DrawableHitCircle circle:
                    // keep approach circle at min size
                    using (circle.BeginAbsoluteSequence(h.StartTime - h.TimePreempt, true))
                        circle.ApproachCircle.Hide();

                    // expand hitcircle scale from 0 to 1
                    using (drawable.BeginAbsoluteSequence(fadeOutStartTime, true))
                        circle.FadeOut(fadeOutDuration);

                    break;
                case DrawableSlider slider:
                    using (slider.BeginAbsoluteSequence(fadeOutStartTime, true))
                        slider.Body.FadeOut(longFadeDuration, Easing.Out);

                    break;
            }
        }
    }
}
