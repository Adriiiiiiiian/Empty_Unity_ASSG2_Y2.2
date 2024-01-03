using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string displayname;
    public string email;

    public User()
    {
        //
    }

    public User(string displayname, string email)
    {

        this.displayname = displayname;
        this.email = email;
    }
}
