/*
 * Copyright (C) 2011 Patrik Akerfeldt
 * Copyright (C) 2011 Jake Wharton
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Android.Support.V4.View;

namespace Com.Flyco.PageIndicator.Indicator.Base
{
	public interface IPageIndicator : ViewPager.IOnPageChangeListener
	{
		/** bind ViewPager */
		void SetViewPager(ViewPager vp);

		/** for special viewpager,such as LooperViewPager */
		void SetViewPager(ViewPager vp, int realCount);

		void SetCurrentItem(int item);
	}
}