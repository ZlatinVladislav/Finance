import React from "react";
import { Tab } from "semantic-ui-react";
import UserProfilePhotos from "./UserProfilePhotos";
import { UserProfile } from "../../app/models/profile";
import { observer } from "mobx-react-lite";

interface Props{
    userProfile:UserProfile;
}

export default observer(function UserProfileContent({userProfile}: Props) {
    const panes = [
        {menuItem: 'About', render: () => <Tab.Pane>About</Tab.Pane>},
        {menuItem: 'Photos', render: () => <UserProfilePhotos userProfile={userProfile}/>},
        {menuItem: 'Events', render: () => <Tab.Pane>Events</Tab.Pane>},
        {menuItem: 'Followers', render: () => <Tab.Pane>Followers</Tab.Pane>},
        {menuItem: 'Following', render: () => <Tab.Pane>Following</Tab.Pane>},
    ];

    return (
        <Tab
            menu={{fluid:true,vertical:true}}
            menuPosition='right'
            panes={panes}
        />
    )
})