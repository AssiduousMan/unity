using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  这个是字幕的类，用来管理字幕的。比如当吃到加攻击的就会显示攻击+多少。

public class Subtitle : MonoBehaviour {

    public Text text;  //  字幕的内容。

    public void SetSubtitleContent(string str) {  //  设置标题的内容。

        this.text.text = str;
        StartCoroutine(ConutDown());

    }

    IEnumerator ConutDown() {  //  倒计时，在规定的秒数之后变为不激活状态。

        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);

    }

	
}
