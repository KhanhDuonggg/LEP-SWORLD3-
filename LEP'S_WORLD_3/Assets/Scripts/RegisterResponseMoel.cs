using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterResponseMoel 
{
    public RegisterResponseMoel(string notification, int status)
    {
        this.notification = notification;
        this.status = status;
    }

    public string notification { get; set; }
    public int status { get; set; }
}
