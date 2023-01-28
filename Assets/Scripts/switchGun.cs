using UnityEngine;

public class switchGun : MonoBehaviour
{
    public int selected = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelected = selected;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            if(selected >= transform.childCount - 1)
                selected = 0;
            else
                selected++;  
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(selected <= 0)
                selected = transform.childCount - 1;
            else
                selected--;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
            selected = 0;

        if(Input.GetKeyDown(KeyCode.Alpha2))
            selected = 1;

        if(Input.GetKeyDown(KeyCode.Alpha3))
            selected = 2;

        if(previousSelected != selected)
            SelectWeapon();
    }

    void SelectWeapon(){
        int i = 0;
        foreach(Transform weapon in transform){
            if(i == selected)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
