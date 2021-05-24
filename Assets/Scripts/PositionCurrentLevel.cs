using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionCurrentLevel : MonoBehaviour
{
    public ScrollRect myScrollRect;
    public RectTransform scrollableContent;

    //private float oneColumnWidth = 0.00004682086f;
    //private float skipwidth = 8 * 0.00004682086f;
    private int i;
    // Start is called before the first frame update
    private void Start()
    {
        ///
        /// 1/21358 = 0.00004682086 = 1columnWidth
        /// for every 25 levels slide through 8 columns
        /// 25 - 8  slide to skipwidth
        /// 50- 16 slide to skipwidth +=skipwidth
        /// 75- 24
        /// 100- 32
        /// 125- 40
        /// 150-48
        /// 175-56
        /// 200-64
        /// 225-72
        /// 250-80
        /// 
        /*       SaveManager.Instance.state.completedLevel = 50;
               SaveManager.Instance.Save();
               myScrollRect.content = scrollableContent;
               i = SaveManager.Instance.state.completedLevel;
               Debug.Log("completed  level no " + i);
               if (i%25 ==0)
               {
                   skipwidth += skipwidth;
                   myScrollRect.horizontalNormalizedPosition = skipwidth;
               }
              Debug.Log("skipwidth is " + skipwidth);

       */

        //SaveManager.Instance.state.completedLevel = 168;
        myScrollRect.content = scrollableContent;
         i = SaveManager.Instance.state.completedLevel;
        Debug.Log("completedlevelis" + i);

        if (i <= 294)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 91f);
        }
        if (i <= 273)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 84f);
        }
        if (i <= 252)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 77f);
        }
        if (i <= 231)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 70f);
        }
        if (i <= 210)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 63f);
        }
        if (i <= 189)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 56f);
        }
        if (i <= 168)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 49f);
        }
        if (i <= 147)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 42f);
        }
        if (i <= 126)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 35f);
        }
        if (i <= 105)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f- 0.0000529521669f) * 28f);
        }
        if (i <= 84)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 21f);
        }
        if (i <= 63)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 14f);
        }
        if (i <= 42)
        {
            myScrollRect.horizontalNormalizedPosition = 0.00444798202f + ((0.0133219955f - 0.0000529521669f) * 7f);
        }
        if (i<=21)
        {
            myScrollRect.horizontalNormalizedPosition = 0f;
        }

/*                          if (i >= 21)
                           {
                               if(i >= 42)
                               {
                                   if(i >= 63)
                                   {
                                       if(i >= 84)
                                       {
                                        if (i>= 105)
                                        {
                                         if(i>=126)
                                         {
                                          if (i >= 126)
                                          {
                                            if (i >= 126)
                                            {
                                        myScrollRect.horizontalNormalizedPosition = 0.017f * 42;
                                    }
                                              else
                                        myScrollRect.horizontalNormalizedPosition = 0.017f * 49;
                                    myScrollRect.horizontalNormalizedPosition = 0.017f * 42;
                                }
                                           else
                                          myScrollRect.horizontalNormalizedPosition = 0.017f * 42;
                            }
                                         else
                                         myScrollRect.horizontalNormalizedPosition = 0.017f * 35;
                        }
                                           myScrollRect.horizontalNormalizedPosition = 0.017f * 28;
                                       }
                                       else
                                   myScrollRect.horizontalNormalizedPosition = 0.017f * 21;
                                   }
                                   else
                                myScrollRect.horizontalNormalizedPosition = 0.017f * 14;
                               }
                               else
                            myScrollRect.horizontalNormalizedPosition = 0.017f * 7;
                          }
 */           
    }
}
