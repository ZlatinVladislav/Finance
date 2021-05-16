import React, { Fragment } from 'react';
import { Segment, Button, Placeholder } from 'semantic-ui-react';

export default function TransactionListItemPlaceholder() {
    return (
        <Fragment>
            <Placeholder fluid style={{ marginTop: 25 }}>
                <Segment.Group>
                    <Segment style={{ minHeight: 110 }}>
                        <Placeholder>
                            <Placeholder.Header image>
                                <Placeholder.Line />
                                <Placeholder.Line />
                            </Placeholder.Header>
                            <Placeholder.Paragraph>
                                <Placeholder.Line />
                            </Placeholder.Paragraph>
                        </Placeholder>
                    </Segment>
                    <Segment>
                        <Placeholder>
                            <Placeholder.Line />
                        </Placeholder>
                    </Segment>
                    <Segment clearing>
                        <Placeholder>
                            <Placeholder.Line />
                        </Placeholder>
                        <Button disabled color='blue' floated='right' content='View' />
                    </Segment>
                </Segment.Group>
            </Placeholder>
        </Fragment>
    );
};
