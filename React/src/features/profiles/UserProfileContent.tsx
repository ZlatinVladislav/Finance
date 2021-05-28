import React from "react";
import { Tab } from "semantic-ui-react";
import UserProfilePhotos from "./UserProfilePhotos";
import { UserProfile } from "../../app/models/profile";
import { observer } from "mobx-react-lite";
import UserProfileAbout from "./UserProfileAbout";

interface Props{
    userProfile:UserProfile;
}

export default observer(function UserProfileContent({userProfile}: Props) {
    const panes = [
        {menuItem: 'About', render: () => <UserProfileAbout userProfile={userProfile}/>},
        {menuItem: 'Photos', render: () => <UserProfilePhotos userProfile={userProfile}/>},
    ];

    return (
        <Tab
            menu={{fluid:true,vertical:true}}
            menuPosition='right'
            panes={panes}
        />
    )
})