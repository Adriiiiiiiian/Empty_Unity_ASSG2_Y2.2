/*
 * Author: Bhoomika & Grace  
 * Date: 2/1/2024
 * Description: handles all the firebase things
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    //Declare variables
    public string displayname;
    public string email;

    public User()
    {
        //
    }

    //Declaration
    public User(string displayname, string email)
    {
        this.displayname = displayname;
        this.email = email;
    }
}
