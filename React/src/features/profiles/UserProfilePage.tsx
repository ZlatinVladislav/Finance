import React, { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import UserProfileHeader from "./UserProfileHeader";
import UserProfileContent from "./UserProfileContent";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default observer(function UserProfilePage() {
    const {userName} = useParams<{ userName: string }>();
    const {userProfileStore} = useStore();
    const {userProfile, loadingProfile, loadUserProfile} = userProfileStore;

    useEffect(() => {
        loadUserProfile(userName);
    }, [loadUserProfile, userName])

    if (loadingProfile) return <LoadingComponent content='Loading user profile...'/>

    return (
        <Grid>
            <Grid.Column width={16}>
                {userProfile &&
                <>
                    <UserProfileHeader userProfile={userProfile}/>
                    <UserProfileContent userProfile={userProfile}/>
                </>
                }
            </Grid.Column>
        </Grid>
    )
})