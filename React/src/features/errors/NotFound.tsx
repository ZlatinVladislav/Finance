import React from "react";
import { Button, Header, Icon, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search'/>
                Not found recheck please and try again
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/transactions' primary>
                    Return to transactions page
                </Button>
            </Segment.Inline>
        </Segment>
    )
}