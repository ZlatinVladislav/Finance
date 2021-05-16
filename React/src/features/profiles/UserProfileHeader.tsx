import React from "react";
import { Button, Divider, Grid, Header, Item, Reveal, Segment, Statistic } from "semantic-ui-react";
import { UserProfile } from "../../app/models/profile";
import { observer } from "mobx-react-lite";

interface Props{
    userProfile:UserProfile;
}

export default observer(function UserProfileHeader({userProfile}:Props) {
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
                <Grid.Column width={4}>
                    <Statistic.Group width={2}>
                        <Statistic label='Followers' value='5'/>
                        <Statistic label='Following' value='42'/>
                    </Statistic.Group>
                    <Divider/>
                    <Reveal animated='move'>
                        <Reveal.Content visible style={{width: '100%'}}>
                            <Button fluid color='teal' content='Following'/>
                        </Reveal.Content>
                        <Reveal.Content hidden style={{width: '100%'}}>
                            <Button basic fluid color={true ? 'red' : 'green'} content={true ? 'unfollow' : 'follow'}/>
                        </Reveal.Content>
                    </Reveal>
                </Grid.Column>
            </Grid>
        </Segment>
    )
})