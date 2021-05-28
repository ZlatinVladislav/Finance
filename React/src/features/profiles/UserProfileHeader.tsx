import React from "react";
import { Grid, Header, Item, Segment } from "semantic-ui-react";
import { UserProfile } from "../../app/models/profile";
import { observer } from "mobx-react-lite";

interface Props {
    userProfile: UserProfile;
}

export default observer(function UserProfileHeader({userProfile}: Props) {
    return (
        <Segment>
            <Grid>
                <Grid.Column width={12}>
                    <Item.Group>
                        <Item>
                            <Item.Image avatar size='small' src={userProfile.image || '/assets/user.png'}/>
                            <Item.Content verticalAlign='middle'>
                                <Header as='h1' content={userProfile.displayName}/>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Grid.Column>
            </Grid>
        </Segment>
    )
})